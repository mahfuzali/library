using Library.Application.Books.ResourceParameters;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{
    public interface ILibraryRepository
    {
        #region Author
        Author GetAuthor(Guid authorId);
        Author GetAuthor(Guid authorId, Guid bookId);
        IEnumerable<Author> GetAuthors();
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool AuthorExists(string firstName, string lastName, DateTimeOffset dateOfBirth);
        #endregion

        #region Book
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooks(Guid authorId);
        IEnumerable<Book> GetBooks(IEnumerable<Guid> bookIds);
        IEnumerable<Book> GetBooks(BooksResourceParameters booksResourceParameters);
        Book GetBook(Guid bookId);
        Book GetBook(Guid authorId, Guid bookId);
        void AddBookAuthor(Book book, Author author);
        void AddBook(Book book);
        void AddBook(Guid authorId, Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        bool BookExists(Guid bookId);
        bool BookExists(string title, string isbn, string publiser);
        #endregion

        bool Save();
    }
}
