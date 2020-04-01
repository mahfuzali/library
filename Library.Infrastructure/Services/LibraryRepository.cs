using Library.Application.Books.ResourceParameters;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class LibraryRepository : ILibraryRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public LibraryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Authors
        public void AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.Any(author => author.AuthorId == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Book)
                    .FirstOrDefault(a => a.AuthorId == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            //return _context.Authors.ToList<Author>();
            return _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book);
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return _context.Authors
                .Where(a => authorIds.Contains(a.AuthorId))
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        #endregion

        #region Books
        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .FirstOrDefault(a => a.BookId == bookId);
        }

        public Book GetBook(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author.AuthorId == authorId)
                    .FirstOrDefault(a => a.BookId == bookId);
                    
        }

        public IEnumerable<Book> GetBooks(IEnumerable<Guid> bookIds)
        {
            if (bookIds == null)
            {
                throw new ArgumentNullException(nameof(bookIds));
            }

            return _context.Books
                .Where(b => bookIds.Contains(b.BookId))
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .OrderBy(b => b.Title)
                .ToList();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author);
        }

        public IEnumerable<Book> GetBooks(BooksResourceParameters booksResourceParameters) {
            if (booksResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(booksResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(booksResourceParameters.Title)
                && string.IsNullOrWhiteSpace(booksResourceParameters.SearchQuery))
            {
                return GetBooks();
            }

            var collectionOfBooks = _context.Books
                                        .Include(a => a.BookAuthors)
                                            .ThenInclude(ba => ba.Author) as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(booksResourceParameters.Title))
            {
                var title = booksResourceParameters.Title.Trim();
                collectionOfBooks = collectionOfBooks.Where(book => book.Title == title);
            }

            if (!string.IsNullOrWhiteSpace(booksResourceParameters.SearchQuery)) 
            {
                var searchQuery = booksResourceParameters.SearchQuery.Trim();
                collectionOfBooks = collectionOfBooks.Where(book => book.Title.Contains(searchQuery)
                    || book.Description.Contains(searchQuery)
                    || book.Publisher.Contains(searchQuery)
                    //|| book.Language.Name.Contains(searchQuery)
                );
            }
            return collectionOfBooks.ToList();
        }

        public bool BookExists(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Books.Any(book => book.BookId == bookId);
        }

        #endregion

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
