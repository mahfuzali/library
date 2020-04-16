using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
                //AuthorId = Guid.Parse("655c99cb-57c6-413a-b008-ebdd5632dcb1"),
                FirstName = "Mahfuz",
                LastName = "Ali",
                DateOfBirth = new DateTime(1990, 12, 25)
            };

            repository.AddAuthor(author);

            var newAuthor = repository.Get(author.AuthorId);


            Assert.Equal(author, newAuthor);
            //Assert.True(newAuthor?.Id > 0);
        }
    }
}
