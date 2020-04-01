using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Dtos.Models
{
    public class BookViewModel
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Publisher { get; set; }

        public string ISBN { get; set; }

        public string Language { get; set; }
    }
}
