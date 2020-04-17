using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.IntegrationTests.Data
{
    public class AuthorRepositoryTests : BaseRepositoryTests
    {
        [Fact]
        public void AddAuthorTest()
        {
            var repository = GetRepository();
            Author author = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };

            repository.Authors.Add(author);
            var newAuthor = repository.Authors.Get(author.AuthorId);

            Assert.Equal(author, newAuthor);
        }

        [Fact]
        public void AddRangeAuthorTest()
        {
            var repository = GetRepository();
            Author author1 = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };
            Author author2 = new Author()
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 07, 23)
            };

            var newAuthors = new List<Author>() { author1, author2 };

            repository.Authors.AddRange(newAuthors);
            var allAuthors = repository.Authors.GetAll();

            Assert.Subset(new HashSet<Author>(newAuthors), new HashSet<Author>(allAuthors));
        }

        [Fact]
        public void UpdateAuthorTest()
        {
            // add an item
            var repository = GetRepository();
            Author author = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };

            repository.Authors.Add(author);

            // detach the item so we get a different instance
            _dbContext.Entry(author).State = EntityState.Detached;

            // fetch the item and update its title
            var fetchedAuthor = repository.Authors.Get(author.AuthorId);
            Assert.NotNull(fetchedAuthor);
            Assert.NotSame(author, fetchedAuthor);

            fetchedAuthor.FirstName = "Muhammed";

            // Update the item
            repository.Authors.Update(fetchedAuthor);
            var updatedAuthor = repository.Authors.Get(author.AuthorId);

            Assert.NotNull(updatedAuthor);
            Assert.NotEqual(author.FirstName, updatedAuthor.FirstName);
            Assert.Equal(author.AuthorId, fetchedAuthor.AuthorId);
        }

        [Fact]
        public void UpdateRangeAuthorTest()
        {
            // add an item
            var repository = GetRepository();
            Author author1 = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };
            Author author2 = new Author()
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1984, 6, 14)
            };

            var newlyAddedAuthors = new List<Author>() { author1, author2 };
            repository.Authors.AddRange(newlyAddedAuthors);

            // detach the item so we get a different instance
            foreach (var author in newlyAddedAuthors)
            {
                _dbContext.Entry(author).State = EntityState.Detached;
            }

            // fetch the item and update its title
            var fetchNewlyAddedAuthors = repository.Authors.Find(a => a.AuthorId == author1.AuthorId || a.AuthorId == author2.AuthorId).ToList();
            Assert.NotNull(fetchNewlyAddedAuthors);
            Assert.NotSame(newlyAddedAuthors, fetchNewlyAddedAuthors);

            fetchNewlyAddedAuthors[0].FirstName = "Muhammed";
            fetchNewlyAddedAuthors[1].FirstName = "Steve";

            // Update the item
            repository.Authors.UpdateRange(fetchNewlyAddedAuthors);

            var fetchUpdatedAuthors = repository.Authors.Find(a => a.AuthorId == author1.AuthorId || a.AuthorId == author2.AuthorId).ToList();
            Assert.Equal(2, fetchUpdatedAuthors.Count());
            Assert.NotEqual(
                new List<string>() { author1.FirstName, author2.FirstName}, 
                new List<string>() { fetchUpdatedAuthors[0].FirstName, fetchUpdatedAuthors[1].LastName}
            );
        }

        [Fact]
        public void DeleteAuthorTest()
        {
            // add an item
            var repository = GetRepository();
            Author author = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };

            repository.Authors.Add(author);

            _dbContext.Entry(author).State = EntityState.Detached;

            // delete the item
            var findAuthor = repository.Authors.SingleOrDefault(a => a.AuthorId == author.AuthorId);
            repository.Authors.Remove(findAuthor);

            // verify it's no longer there
            var allAuthors = repository.Authors.GetAll();
            Assert.DoesNotContain(allAuthors, a => a.AuthorId == author.AuthorId);
        }

        [Fact]
        public void DeleteRangeAuthorTest()
        {
            // add an item
            var repository = GetRepository();
            Author author1 = new Author()
            {
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };
            Author author2 = new Author()
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1984, 6, 14)
            };

            var newlyAddedAuthors = new List<Author>() { author1, author2 };
            repository.Authors.AddRange(newlyAddedAuthors);

            foreach (var author in newlyAddedAuthors)
            {
                _dbContext.Entry(author).State = EntityState.Detached;
            }

            // delete the item
            var fetchNewlyAddedAuthors = repository.Authors.Find(a => a.AuthorId == author1.AuthorId || a.AuthorId == author2.AuthorId).ToList();
            repository.Authors.RemoveRange(fetchNewlyAddedAuthors);

            // verify it's no longer there
            var deletedAuthors = repository.Authors.Find(a => a.AuthorId == author1.AuthorId || a.AuthorId == author2.AuthorId).ToList(); ;
            Assert.False(deletedAuthors.Any());
        }
    }
}