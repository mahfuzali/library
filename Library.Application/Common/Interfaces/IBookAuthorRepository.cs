using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Common.Interfaces
{
    public interface IBookAuthorRepository : IRepository<BookAuthor>
    {
        void AddBookAuthor(Book book, Author author);
    }
}
