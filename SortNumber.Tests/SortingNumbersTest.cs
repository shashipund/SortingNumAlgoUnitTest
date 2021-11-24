using System;
using Xunit;
using CoreApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoreApi.Repository;
using System.Linq;
using System.Text;

namespace SortNumber.Tests
{
    public class SortingNumbersTest
    {
        private readonly HttpClient _client;
        private ISortNumber sort;
        public SortingNumbersTest(ISortNumber _sort)
        {
            this.sort = _sort;
            _client= new HttpClient ();
        }


        [Theory]
        [InlineData("Get","3,4,2,5,6,1")]
        public async Task TestSortNumberGetCall(string method, string numLine=null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"http://localhost:5000/SortNumbers/{numLine}");
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [InlineData("3,4,2,5,6,1")]
        public void TestSortNumAfterResult(string numberLine)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] numberArray = numberLine.Split(' ');
            int[] numbers = numberArray.Select(int.Parse).ToArray();

            string[] sortedNumbers = sort.SortNumber(numbers);
            foreach (var item in sortedNumbers)
                stringBuilder.Append(item + " ");

                Assert.Equal("1 2 3 4 5 6", stringBuilder.ToString());
                
        }
    }
}
