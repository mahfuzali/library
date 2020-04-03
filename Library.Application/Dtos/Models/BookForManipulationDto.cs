using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Application.Common.ValidationAttributes;

namespace Library.Application.Dtos.Models
{
    [BookTitleMustBeDifferentFromDescription(
          ErrorMessage = "Title must be different from description.")]
    public abstract class BookForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "The description shouldn't have more than 1500 characters.")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "You should fill out a publisher.")]
        [MaxLength(100, ErrorMessage = "The publisher shouldn't have more than 100 characters.")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "You should fill out a ISBN.")]
        [MaxLength(14, ErrorMessage = "The ISBN shouldn't have more than 14 characters. Format xxx-xxxxxxxxxx")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "You should fill out genres.")]
        public ICollection<string> Genres { get; set; }
            = new List<string>();

        [Required(ErrorMessage = "You should fill out a language.")]
        [MaxLength(100, ErrorMessage = "The language shouldn't have more than 100 characters.")]
        public string Language { get; set; }


        public ICollection<AuthorForCreationDto> Authors { get; set; }
                = new List<AuthorForCreationDto>();
    }
}
