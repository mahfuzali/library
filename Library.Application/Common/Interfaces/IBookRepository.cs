using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooks();

        Task<IEnumerable<Book>> GetBooks(Guid authorId);

        Task<IEnumerable<Book>> GetBooks(IEnumerable<Guid> bookIds);

        PagedList<Book> GetBooks(BooksResourceParameters booksResourceParameters);

        Task<Book> GetBook(Guid bookId);

        Task<Book> GetABookByAnAuthor(Guid authorId, Guid bookId);

        void AddBook(Guid authorId, Book book);

        Task<IEnumerable<Book>> GetBooksOfAnAuthor(Guid authorId);


    }
}
