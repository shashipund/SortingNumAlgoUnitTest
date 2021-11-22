using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Microsoft.Net.Http.Headers;
using CoreApi.Repository;
using System;

namespace CoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SortingController : ControllerBase
    {

        private ISortNumber sort;
        public SortingController(ISortNumber _sort)
        {
            this.sort = _sort;
        }


        [HttpGet("/SortNumbers")]
        public FileStreamResult SortNumbers(string numberLine)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] numberArray = numberLine.Split(' ');
            int[] numbers = numberArray.Select(int.Parse).ToArray();

            string[] sortedNumbers = sort.SortNumber(numbers);

            foreach (var item in sortedNumbers)
                stringBuilder.Append(item + " ");

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(stringBuilder.ToString()));

            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "Result.txt"
            };
        }

        
        [HttpPost("/LoadFile"), DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = Path.Combine("Files", "");
                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
