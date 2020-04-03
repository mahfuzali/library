using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Library.Application.Dtos.Models;

namespace Library.Application.Common.ValidationAttributes
{
    public class BookTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var course = (BookForManipulationDto)validationContext.ObjectInstance;

            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(BookForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
