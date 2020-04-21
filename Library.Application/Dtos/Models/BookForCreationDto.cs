using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Application.Dtos.Models
{
    public class BookForCreationDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a description.")]
        public override string Description { get => base.Description; set => base.Description = value; }

        //[Required(ErrorMessage = "You should fill out a title.")]
        //[MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        //public string Title { get; set; }

        //[Required(ErrorMessage = "You should fill out a description.")]
        //[MaxLength(1500, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        //public string Description { get; set; }

        //[Required(ErrorMessage = "You should fill out a publisher.")]
        //public string Publisher { get; set; }

        //[Required(ErrorMessage = "You should fill out a ISBN.")]
        //public string ISBN { get; set; }

        //[Required(ErrorMessage = "You should fill out a genres.")]
        //public ICollection<string> Genres { get; set; }
        //    = new List<string>();

        //[Required(ErrorMessage = "You should fill out a description.")]
        //public string Language { get; set; }

        //public ICollection<AuthorForCreationDto> Authors { get; set; }
        //        = new List<AuthorForCreationDto>();

    }
}
