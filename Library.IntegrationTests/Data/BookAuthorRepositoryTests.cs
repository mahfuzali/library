using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Library.IntegrationTests.Data
{
    public class BookAuthorRepositoryTests : BaseRepositoryTests
    {
        [Fact]
        public void AddBookAuthorTest()
        {
            var repository = GetRepository();
            Book book = new Book()
            {
                Title = "Example Title",
                Description = "Example Description",
                Publisher = "Example Publisher",
                ISBN = "000-0000000000",
                Genres = new List<Genre>()
                {
                    Genre.For("Exmaple Genre"),
                },
                Language = Language.For("Example Language")
            };

            repository.Books.Add(book);

            Author author = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };

            repository.Authors.Add(author);

            BookAuthor ba = new BookAuthor()
            {
                BookId = book.BookId,
                Book = book,
                AuthorId = author.AuthorId,
                Author = author
            };

            repository.BookAuthors.Add(ba);

            _dbContext.Entry(ba).State = EntityState.Detached;

            var fetchBook = repository.Books.Get(book.BookId);
            var fetchAuthor = repository.Authors.Get(author.AuthorId);

            var fetchBookAuthor = repository.BookAuthors.SingleOrDefault(ba => ba.BookId == book.BookId && ba.AuthorId == author.AuthorId);

            Assert.Equal(author.AuthorId, fetchBookAuthor.AuthorId);
            Assert.Equal(book.BookId, fetchBookAuthor.BookId);
        }
    }
}
