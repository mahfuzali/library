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

        public Author GetAuthor(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            var bookAuthor = _context.BookAuthors.FirstOrDefault(b => b.AuthorId == authorId && b.BookId == bookId);

            return _context.Authors
                        .Include(ba => ba.BookAuthors)
                            .ThenInclude(b => b.Book)
                    .FirstOrDefault(a => a.AuthorId == bookAuthor.AuthorId);
        }
        
        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            author.AuthorId = Guid.NewGuid();

            _context.Authors.Add(author);
        }

        public void UpdateAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Update(author);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.Any(author => author.AuthorId == authorId);
        }

        public bool AuthorExists(string firstName, string lastName, DateTimeOffset dateOfBirth) 
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) && dateOfBirth == null)
            {
                throw new ArgumentNullException("Invalid input");
            }

            return _context.Authors.Any(author =>
                                            author.FirstName == firstName &&
                                            author.LastName == lastName && 
                                            author.DateOfBirth == dateOfBirth);
        }

        #endregion

        #region BookAuthor
        public void AddBookAuthor(Book book, Author author)
        {
            if (book == null )
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            BookAuthor ba = new BookAuthor
            {
                BookId = book.BookId,
                Book = book,
                AuthorId = author.AuthorId,
                Author = author
            };

            _context.BookAuthors.Add(ba);
        }
        #endregion

        #region Books

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

            var book = _context.BookAuthors.FirstOrDefault(b => b.AuthorId == authorId && b.BookId == bookId);

            return _context.Books
                        .Include(ba => ba.BookAuthors)
                            .ThenInclude(a => a.Author)
                    .FirstOrDefault(b => b.BookId == book.BookId);

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

        public IEnumerable<Book> GetBooks(BooksResourceParameters booksResourceParameters)
        {
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

        public IEnumerable<Book> GetBooks(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            var collectionOfBookAuthors = _context.BookAuthors
                                                .Where(author => author.AuthorId == authorId).Select(a => a.BookId).ToList();

            return _context.Books.Where(b => collectionOfBookAuthors.Contains(b.BookId))
                                        .Include(a => a.BookAuthors)
                                            .ThenInclude(ba => ba.Author);
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.BookId = Guid.NewGuid();

            _context.Books.Add(book);
        }

        public void AddBook(Guid authorId, Book book)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.BookId = Guid.NewGuid();

            var author = _context.Authors.FirstOrDefault(author => author.AuthorId == authorId); 

            BookAuthor ba = new BookAuthor
            {
                BookId = book.BookId,
                Book = book,
                AuthorId = author.AuthorId,
                Author = author
            };

            _context.Books.Add(book);

            _context.BookAuthors.Add(ba);
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Update(book);
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Remove(book);
        }

        public bool BookExists(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Books.Any(book => book.BookId == bookId);
        }

        public bool BookExists(string title, string isbn, string publiser) 
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(isbn) && string.IsNullOrWhiteSpace(publiser))
            {
                throw new ArgumentNullException("Invalid input");
            }

            return _context.Books.Any(book => 
                                            book.Title == title && 
                                            book.ISBN == isbn && 
                                            book.Publisher == publiser);
        }

        #endregion

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
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
