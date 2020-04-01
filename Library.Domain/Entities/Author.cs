using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Domain.Entities
{
    public class Author : AuditableEntity
    {
        public Guid AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public DateTimeOffset DateOfDeath { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
            = new List<BookAuthor>();
    }
}
