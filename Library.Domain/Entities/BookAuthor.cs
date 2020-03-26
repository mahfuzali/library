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
        //[Key]
        public Guid BookId { get; set; }

        //[ForeignKey("BookId")]
        public Book Book { get; set; }

        //[Key]
        public Guid AuthorId { get; set; }

        //[ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
