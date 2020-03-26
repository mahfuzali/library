using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Domain.Entities
{
    public class Book : AuditableEntity
    {
        //[Key]
        public Guid Id { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Title { get; set; }

        //[MaxLength(1500)]
        public string Description { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
                = new List<BookAuthor>();
    }
}
