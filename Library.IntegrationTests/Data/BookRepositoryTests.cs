using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.IntegrationTests.Data
{
    public class BookRepositoryTests : BaseRepositoryTests
    {
        [Fact]
        public void AddBookTest()
        {
            var repository = GetRepository();
            Book book = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };

            repository.Books.Add(book);
            var newBook = repository.Books.Get(book.BookId);

            Assert.Equal(book, newBook);
        }

        [Fact]
        public void AddRangeAuthorTest()
        {
            var repository = GetRepository();
            Book book1 = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };
            Book book2 = new Book()
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

            var newBooks = new List<Book>() { book1, book2 };

            repository.Books.AddRange(newBooks);
            var allBooks = repository.Books.GetAll();

            Assert.Subset(new HashSet<Book>(newBooks), new HashSet<Book>(allBooks));
        }

        [Fact]
        public void UpdateBookTest()
        {
            // add an item
            var repository = GetRepository();
            Book book = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };

            repository.Books.Add(book);

            // detach the item so we get a different instance
            _dbContext.Entry(book).State = EntityState.Detached;

            // fetch the item and update its title
            var fetchedBook = repository.Books.Get(book.BookId);
            Assert.NotNull(fetchedBook);
            Assert.NotSame(book, fetchedBook);

            fetchedBook.Title = "Muhammed";

            // Update the item
            repository.Books.Update(fetchedBook);
            var updateBook = repository.Books.Get(book.BookId);

            Assert.NotNull(updateBook);
            Assert.NotEqual(book.Title, updateBook.Title);
            Assert.Equal(book.BookId, fetchedBook.BookId);
        }

        [Fact]
        public void UpdateRangeBookTest()
        {
            // add an item
            var repository = GetRepository();
            Book book1 = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };
            Book book2 = new Book()
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

            var newlyAddedBooks = new List<Book>() { book1, book2 };
            repository.Books.AddRange(newlyAddedBooks);

            // detach the item so we get a different instance
            foreach (var book in newlyAddedBooks)
            {
                _dbContext.Entry(book).State = EntityState.Detached;
            }

            // fetch the item and update its title
            var fetchNewlyAddedBooks = repository.Books.Find(b => b.BookId == book1.BookId || b.BookId == book2.BookId).ToList();
            Assert.NotNull(fetchNewlyAddedBooks);
            Assert.NotSame(newlyAddedBooks, fetchNewlyAddedBooks);

            fetchNewlyAddedBooks[0].Title = "Algorithm";
            fetchNewlyAddedBooks[1].Title = "Just title";

            // Update the item
            repository.Books.UpdateRange(fetchNewlyAddedBooks);

            var fetchUpdatedBooks = repository.Books.Find(b => b.BookId == book1.BookId || b.BookId == book2.BookId).ToList();
            Assert.Equal(2, fetchUpdatedBooks.Count());
            Assert.NotEqual(
                new List<string>() { book1.Title, book2.Title },
                new List<string>() { fetchUpdatedBooks[0].Title, fetchUpdatedBooks[1].Title }
            );
        }

        [Fact]
        public void DeleteBookTest()
        {
            // add an item
            var repository = GetRepository();
            Book book = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };

            repository.Books.Add(book);

            _dbContext.Entry(book).State = EntityState.Detached;

            // delete the item
            var findBook = repository.Books.Get(book.BookId);
            repository.Books.Remove(findBook);

            // verify it's no longer there
            var allBooks = repository.Books.GetAll();
            Assert.DoesNotContain(allBooks, b => b.BookId == book.BookId);
        }

        [Fact]
        public void DeleteRangeBookTest()
        {
            // add an item
            var repository = GetRepository();
            Book book1 = new Book()
            {
                Title = "Algorithms to Live by: The Computer Science of Human Decisions",
                Description = "A fascinating exploration of how insights from computer algorithms can be applied to our everyday lives, helping to solve common decision-making problems and illuminate the workings of the human mind",
                Publisher = "Macmillan USA",
                ISBN = "978-1627790369",
                Genres = new List<Genre>()
                {
                    Genre.For("Psychology"),
                    Genre.For("Business Decision Making Skills"),
                    Genre.For("Maths")
                },
                Language = Language.For("English")
            };
            Book book2 = new Book()
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

            var newlyAddedBooks = new List<Book>() { book1, book2 };
            repository.Books.AddRange(newlyAddedBooks);

            foreach (var book in newlyAddedBooks)
            {
                _dbContext.Entry(book).State = EntityState.Detached;
            }

            // delete the item
            var fetchNewlyAddedBooks = repository.Books.Find(b => b.BookId == book1.BookId || b.BookId == book2.BookId).ToList();
            repository.Books.RemoveRange(fetchNewlyAddedBooks);

            // verify it's no longer there
            var deletedBooks = repository.Books.Find(b => b.BookId == book1.BookId || b.BookId == book2.BookId).ToList(); ;
            Assert.False(deletedBooks.Any());
        }
    }
}
