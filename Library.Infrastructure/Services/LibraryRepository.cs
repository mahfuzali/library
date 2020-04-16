using Library.Application.Authors.Models;
using Library.Application.Authors.ResourceParameters;
using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Common.Helpers;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class LibraryRepository : ILibraryRepository, IDisposable
    {
        private ApplicationDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public LibraryRepository(ApplicationDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ??
                                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        #region Authors
        public async Task<Author> GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Book)
                    .FirstOrDefaultAsync(a => a.AuthorId == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book).ToListAsync();
        }

        public PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        {
            if (authorsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(authorsResourceParameters));
            }

            var collectionOfAuthors = _context.Authors
                                        .Include(ba => ba.BookAuthors)
                                            .ThenInclude(b => b.Book) as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.Name))
            {
                var name = authorsResourceParameters.Name.Trim();
                collectionOfAuthors = collectionOfAuthors.Where(author => author.FirstName == name || author.LastName == name);
            }

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                var searchQuery = authorsResourceParameters.SearchQuery.Trim();
                collectionOfAuthors = collectionOfAuthors.Where(author =>
                    author.FirstName.Contains(searchQuery) ||
                    author.LastName.Contains(searchQuery)
                );
            }

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.OrderBy)) 
            {
                // get property mapping dictionary
                var authorPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<AuthorDto, Author>();

                collectionOfAuthors = collectionOfAuthors.ApplySort(authorsResourceParameters.OrderBy,
                    authorPropertyMappingDictionary);
            }

            return  PagedList<Author>.Create(collectionOfAuthors,
                        authorsResourceParameters.PageNumber,
                        authorsResourceParameters.PageSize);
        }

        public async Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return await _context.Authors
                .Where(a => authorIds.Contains(a.AuthorId))
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToListAsync();
        }

        public async Task<Author> GetAuthor(Guid authorId, Guid bookId)
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

            return await _context.Authors
                        .Include(ba => ba.BookAuthors)
                            .ThenInclude(b => b.Book)
                    .FirstOrDefaultAsync(a => a.AuthorId == bookAuthor.AuthorId);
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

        public async Task<bool> AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors.AnyAsync(author => author.AuthorId == authorId);
        }

        public async Task<bool> AuthorExists(string firstName, string lastName, DateTimeOffset dateOfBirth) 
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) && dateOfBirth == null)
            {
                throw new ArgumentNullException("Invalid input");
            }

            return await _context.Authors.AnyAsync(author =>
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

        public async Task<Book> GetBook(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return await _context.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .FirstOrDefaultAsync(a => a.BookId == bookId);
        }

        public async Task<Book> GetBook(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            var book = await _context.BookAuthors.FirstOrDefaultAsync(b => b.AuthorId == authorId && b.BookId == bookId);

            return await _context.Books
                        .Include(ba => ba.BookAuthors)
                            .ThenInclude(a => a.Author)
                    .FirstOrDefaultAsync(b => b.BookId == book.BookId);
        }

        public async Task<IEnumerable<Book>> GetBooks(IEnumerable<Guid> bookIds)
        {
            if (bookIds == null)
            {
                throw new ArgumentNullException(nameof(bookIds));
            }

            return await _context.Books
                            .Where(b => bookIds.Contains(b.BookId))
                                .Include(b => b.BookAuthors)
                                .ThenInclude(ba => ba.Book)
                            .OrderBy(b => b.Title)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author).ToListAsync();
        }
        
        public PagedList<Book> GetBooks(BooksResourceParameters booksResourceParameters)
        {
            if (booksResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(booksResourceParameters));
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

            if (!string.IsNullOrWhiteSpace(booksResourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var bookPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<BookDto, Book>();

                collectionOfBooks = collectionOfBooks.ApplySort(booksResourceParameters.OrderBy,
                    bookPropertyMappingDictionary);
            }


            return PagedList<Book>.Create(collectionOfBooks,
                booksResourceParameters.PageNumber,
                booksResourceParameters.PageSize);
        }

        public async Task<IEnumerable<Book>> GetBooks(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            var collectionOfBookAuthors = _context.BookAuthors
                                                .Where(author => author.AuthorId == authorId).Select(a => a.BookId).ToList();

            return await _context.Books.Where(b => collectionOfBookAuthors.Contains(b.BookId))
                                        .Include(a => a.BookAuthors)
                                            .ThenInclude(ba => ba.Author).ToListAsync();
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

        public async Task<bool> BookExists(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return await _context.Books.AnyAsync(book => book.BookId == bookId);
        }

        public async Task<bool> BookExists(string title, string isbn, string publiser) 
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(isbn) && string.IsNullOrWhiteSpace(publiser))
            {
                throw new ArgumentNullException("Invalid input");
            }

            return await _context.Books.AnyAsync(book => 
                                            book.Title == title && 
                                            book.ISBN == isbn && 
                                            book.Publisher == publiser);
        }

        #endregion

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
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
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }

    }
}
