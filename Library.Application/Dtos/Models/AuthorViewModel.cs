using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Dtos.Models
{
    public class AuthorViewModel
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
