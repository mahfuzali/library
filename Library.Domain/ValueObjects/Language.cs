using Library.Domain.Common;
using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Language : ValueObject
    {
        private Language()
        {
        }

        public static Language For(string languageString)
        {
            var language = new Language();

            try
            {
                language.Name = languageString.Trim();
            }
            catch (Exception ex)
            {
                throw new LanguageInvalidException(languageString, ex);
            }

            return language;
        }

        public string Name { get; set; }

        public static implicit operator string(Language genre)
        {
            return genre.ToString();
        }

        public static explicit operator Language(string genreString)
        {
            return For(genreString);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
