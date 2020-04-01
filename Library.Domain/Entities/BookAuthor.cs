using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Domain.Entities
{
    public class BookAuthor : AuditableEntity
    {
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
