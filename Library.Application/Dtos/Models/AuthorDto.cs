using Library.Application.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Authors.Models
{
    public class AuthorDto
    {
        public Guid AuthorId { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }

        public string DateOfBirth { get; set; }

        public string DateOfDeath { get; set; }

        public ICollection<BookViewModel> Books { get; set; }
            = new List<BookViewModel>();
    }
}
