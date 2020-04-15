using Library.Application.Authors.ResourceParameters;
using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public interface ILibraryRepository
    {
        #region Author
        Task<Author> GetAuthor(Guid authorId);
        Task<Author> GetAuthor(Guid authorId, Guid bookId);
        Task<IEnumerable<Author>> GetAuthors();
        Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds);
        //Task<IEnumerable<Author>> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);

        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        Task<bool> AuthorExists(Guid authorId);
        Task<bool> AuthorExists(string firstName, string lastName, DateTimeOffset dateOfBirth);
        #endregion

        #region Book
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBooks(Guid authorId);
        Task<IEnumerable<Book>> GetBooks(IEnumerable<Guid> bookIds);
        //Task<IEnumerable<Book>> GetBooks(BooksResourceParameters booksResourceParameters);
        PagedList<Book> GetBooks(BooksResourceParameters booksResourceParameters);
        Task<Book> GetBook(Guid bookId);
        Task<Book> GetBook(Guid authorId, Guid bookId);
        void AddBookAuthor(Book book, Author author);
        void AddBook(Book book);
        void AddBook(Guid authorId, Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        Task<bool> BookExists(Guid bookId);
        Task<bool> BookExists(string title, string isbn, string publiser);
        #endregion

        //bool Save();

        Task<bool> SaveAsync();
    }
}
