using System;
using Xunit;
using CoreApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SortNumber.Tests
{
    public class SortingNumbersTest
    {
        private readonly HttpClient _client;
        
        public SortingNumbersTest()
        {
            
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
    }
}
