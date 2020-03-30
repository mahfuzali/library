using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{
    public interface ILibraryRepository
    {
        IEnumerable<Book> GetBooks(IEnumerable<Guid> bookIds);
        IEnumerable<Book> GetBooks();
        Book GetBook(Guid bookId);
        Book GetBook(Guid authorId, Guid bookId);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool Save();
    }
}
