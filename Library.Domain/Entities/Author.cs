using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Domain.Entities
{
    public class Author : AuditableEntity
    {
        //[Key]
        public Guid Id { get; set; }

        //[Required]
        //[MaxLength(50)]
        public string FirstName { get; set; }

        //[Required]
        //[MaxLength(50)]
        public string LastName { get; set; }

        //[Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
            = new List<BookAuthor>();
    }
}
