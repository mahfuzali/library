using Library.API;
using Library.Application.Common.Models.Requests;
using Library.Application.Common.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.FunctionalTests.Api
{
    public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _client;

        public BaseControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        //public async Task ReturnsAuthTokenTest()
        //{
        //    var result = await GetJwtAsync();
        //    Assert.True(result is string);
        //}

        public async Task<string> GetJwtAsync()
        {
            var json = JsonConvert.SerializeObject(new LoginModel()
            {
                Username = "mahfuzali",
                Password = "Library!1"
            });

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/authenticate/login", stringContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthSuccessResponse>(stringResponse);

            return result.Token;
        }
    }
}
