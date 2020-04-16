using Library.API;
using Library.Application.Authors.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.FunctionalTests.Api
{
    public class AuthorsControllerTests : BaseControllerTests
    {
        public AuthorsControllerTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task ReturnsAuthorsTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
            var response = await _client.GetAsync("/api/authors");

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AuthorDto>>(stringResponse).ToList();

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task ReturnsAnAuthorTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string authorId = SeedData.author3.AuthorId.ToString();
            var response = await _client.GetAsync($"/api/authors/{authorId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthorDto>(stringResponse);


            Assert.NotNull(result);
            Assert.Equal(SeedData.author3.FirstName + " " + SeedData.author3.LastName, result.Name);
        }
    }
}
