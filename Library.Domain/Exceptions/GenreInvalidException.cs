using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class GenreInvalidException: Exception
    {
        public GenreInvalidException(string genre, Exception ex)
            : base($"Genre \"{genre}\" is invalid.", ex)
        {
        }
    }
}
