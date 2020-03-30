using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
                = new List<BookAuthor>();
    }
}
