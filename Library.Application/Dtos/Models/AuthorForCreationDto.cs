using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "You should fill out a firstname.")]
        [MaxLength(50, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You should fill out a lastname.")]
        [MaxLength(50, ErrorMessage = "The title shouldn't have more than 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You should fill out a date of birth.")]
        public DateTimeOffset DateOfBirth { get; set; }

        public DateTimeOffset DateOfDeath { get; set; }

    }
}
