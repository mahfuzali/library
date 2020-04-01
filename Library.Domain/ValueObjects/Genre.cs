using Library.Domain.Common;
using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Genre: ValueObject
    {
        private Genre()
        {
        }

        public static Genre For(string genreString)
        {
            var genre = new Genre();

            try
            {
                genre.Name = genreString.Trim();
            }
            catch (Exception ex)
            {
                throw new GenreInvalidException(genreString, ex);
            }

            return genre;
        }

        //public Guid Id { get; set; }

        public string Name { get; set; }

        public static implicit operator string(Genre genre)
        {
            return genre.ToString();
        }

        public static explicit operator Genre(string genreString)
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
