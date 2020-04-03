using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Dtos.Models
{
    public class BookForCreationForAuthorDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Publisher { get; set; }

        public string ISBN { get; set; }

        public ICollection<string> Genres { get; set; }
            = new List<string>();

        public string Language { get; set; }
    }
}
