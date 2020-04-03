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

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public string Publisher { get; set; }

        public string ISBN { get; set; }

        [Required]
        public ICollection<Genre> Genres { get; set; } 
            = new List<Genre>();

        [Required]
        public Language Language { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
                = new List<BookAuthor>();
    }
}
