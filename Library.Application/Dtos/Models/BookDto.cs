using Library.Application.Authors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Dtos.Models
{
    public class BookDto
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<AuthorViewModel> Authors { get; set; }
            = new List<AuthorViewModel>();
    }
}
