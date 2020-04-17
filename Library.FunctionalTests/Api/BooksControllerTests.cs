using Library.API;
using Library.Application.Dtos.Models;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
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
    public class BooksControllerTests : BaseControllerTests
    {
        public BooksControllerTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task ReturnsBooksTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
            var response = await _client.GetAsync("/api/books");

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BookDto>>(stringResponse).ToList();

            Assert.Equal(9, result.Count());
        }

        [Fact]
        public async Task ReturnsABookTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
            
            string bookId = SeedData.book1.BookId.ToString();
            var response = await _client.GetAsync($"/api/books/{bookId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookDto>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(SeedData.book1.Title, result.Title);
            Assert.True(result.Authors.Count() > 0);
        }

        [Fact]
        public async Task AddABookTest()
        {
            BookForCreationDto newBook = new BookForCreationDto()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<string>()
                {
                    "Psychology",
                    "Business Decision Making Skills",
                    "Maths"
                },
                Language = "English",
                Authors = new List<AuthorForCreationDto>()
                {
                    new AuthorForCreationDto()
                    {
                        FirstName = "Brian",
                        LastName = "Christian",
                        DateOfBirth = DateTimeOffset.Parse("1984-07-28T00:00:00.000Z")
                    },
                    new AuthorForCreationDto()
                    {
                        FirstName = "Tom",
                        LastName = "Griffiths",
                        DateOfBirth = DateTimeOffset.Parse("1987-08-02T00:00:00.000Z")
                    }
                }
            };

            var json = JsonConvert.SerializeObject(newBook);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            var response = await _client.PostAsync("/api/books", stringContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookDto>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(newBook.Title, result.Title);
            Assert.True(result.Authors.Count() == newBook.Authors.Count());
            Assert.True(result.Genres.Count() == newBook.Genres.Count());
            Assert.True(result.Language == newBook.Language);
        }

        [Fact]
        public async Task UpdateABookTest()
        {
            BookForUpdateDto book = new BookForUpdateDto() 
            {
                Title = SeedData.book1.Title,
                Description = "Test Description",
                Publisher = SeedData.book1.Publisher,
                ISBN = SeedData.book1.ISBN,
                Genres = SeedData.book1.Genres.Select(g => g.Name).ToList(),
                Language = SeedData.book1.Language,
                Authors = new List<AuthorForCreationDto>()
                {
                    new AuthorForCreationDto()
                    {
                        FirstName = SeedData.author1.FirstName,
                        LastName = SeedData.author1.LastName,
                        DateOfBirth = SeedData.author1.DateOfBirth
                    }
                }
            };

            var json = JsonConvert.SerializeObject(book);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string bookId = SeedData.book1.BookId.ToString();
            var response = await _client.PutAsync($"/api/books/{bookId}", stringContent);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteABookTest()
        {
            _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", await GetJwtAsync());

            string bookId = SeedData.book2.BookId.ToString();
            var response = await _client.DeleteAsync($"/api/books/{bookId}");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

    }
}
