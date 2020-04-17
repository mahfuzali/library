using Library.API;
using Library.Application.Authors.Models;
using Library.Application.Common.Models.Requests;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [Fact]
        public async Task ReturnsBooksOfAnAuthorTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string authorId = SeedData.harryPotterAuthor.AuthorId.ToString();
            var response = await _client.GetAsync($"/api/authors/{authorId}/books");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BookViewModel>>(stringResponse);


            Assert.NotNull(result);
            Assert.Equal(7, result.Count());
        }

        [Fact]
        public async Task ReturnsABookOfAnAuthorTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string authorId = SeedData.harryPotterAuthor.AuthorId.ToString();
            string bookId = SeedData.harryPotterBook1.BookId.ToString();
            var response = await _client.GetAsync($"/api/authors/{authorId}/books/{bookId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookViewModel>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(SeedData.harryPotterBook1.Title, result.Title);
        }


        [Fact]
        public async Task AddAnBookOfAnAuthorTest()
        {
            BookForCreationForAuthorDto newBook = new BookForCreationForAuthorDto()
            {
                Title = "Fear: Trump in the White House",
                Description = "With authoritative reporting honed through eight presidencies from Nixon to Obama, author Bob Woodward reveals in unprecedented detail the harrowing life inside President Donald Trump’s White House and precisely how he makes decisions on major foreign and domestic policies. Woodward draws from hundreds of hours of interviews with firsthand sources, meeting notes, personal diaries, files and documents. The focus is on the explosive debates and the decision-making in the Oval Office, the Situation Room, Air Force One and the White House residence.",
                Publisher = "Simon & Schuster UK",
                ISBN = "978-1471181290",
                Genres = new List<string>()
                {
                    "Political Science"
                },
                Language = "English"
            };

            var json = JsonConvert.SerializeObject(newBook);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string authorId = SeedData.author2.AuthorId.ToString();

            var response = await _client.PostAsync($"/api/authors/{authorId}/books", stringContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookDto>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(newBook.Title, result.Title);
            Assert.True(result.Genres.Count() == newBook.Genres.Count());
            Assert.True(result.Language == newBook.Language);
        }


        [Fact]
        public async Task UpdateABookViaPatachTest()
        {
            //BookForUpdateDto book = new BookForUpdateDto()
            //{
            //    Title = SeedData.book1.Title,
            //    Description = "Test Description",
            //    Publisher = SeedData.book1.Publisher,
            //    ISBN = SeedData.book1.ISBN,
            //    Genres = SeedData.book1.Genres.Select(g => g.Name).ToList(),
            //    Language = SeedData.book1.Language,
            //    Authors = new List<AuthorForCreationDto>()
            //    {
            //        new AuthorForCreationDto()
            //        {
            //            FirstName = SeedData.author1.FirstName,
            //            LastName = SeedData.author1.LastName,
            //            DateOfBirth = SeedData.author1.DateOfBirth
            //        }
            //    }
            //};


            PatchModel patchBook = new PatchModel()
            {
                Op = "replace",
                Path = "/title",
                Value = "Updated title"
            };


            var json = JsonConvert.SerializeObject(patchBook);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json-patch+json");

            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json-patch+json"));

            string authorId = SeedData.author1.AuthorId.ToString();
            string bookId = SeedData.book1.BookId.ToString();

            var response = await _client.PatchAsync($"/api/authors/{authorId}/books/{bookId}", stringContent);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        public async Task DeleteAnAuthorTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string authorId = SeedData.harryPotterAuthor.AuthorId.ToString();
            var response = await _client.DeleteAsync($"/api/authors/{authorId}");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }
    }
}
