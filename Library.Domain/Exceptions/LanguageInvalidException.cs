using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Exceptions
{
    public class LanguageInvalidException: Exception
    {
        public LanguageInvalidException(string language, Exception ex)
            : base($"Language \"{language}\" is invalid.", ex)
        {
        }
    }
}
