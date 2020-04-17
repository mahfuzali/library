using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Application.Common.Interfaces;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Common.Helpers;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context, IPropertyMappingService propertyMappingService)
            : base(context, propertyMappingService)
        {
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
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

            var author = ApplicationDbContext.Authors.FirstOrDefault(author => author.AuthorId == authorId);

            BookAuthor ba = new BookAuthor
            {
                BookId = book.BookId,
                Book = book,
                AuthorId = author.AuthorId,
                Author = author
            };

            ApplicationDbContext.Books.Add(book);

            ApplicationDbContext.BookAuthors.Add(ba);
        }

        public async Task<Book> GetBook(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return await ApplicationDbContext.Books
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

            var book = await ApplicationDbContext.BookAuthors.FirstOrDefaultAsync(b => b.AuthorId == authorId && b.BookId == bookId);

            return await ApplicationDbContext.Books
                        .Include(ba => ba.BookAuthors)
                            .ThenInclude(a => a.Author)
                    .FirstOrDefaultAsync(b => b.BookId == book.BookId);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await ApplicationDbContext.Books
                        .Include(a => a.BookAuthors)
                        .ThenInclude(ba => ba.Author).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            var collectionOfBookAuthors = ApplicationDbContext.BookAuthors
                                            .Where(author => author.AuthorId == authorId)
                                            .Select(a => a.BookId).ToList();

            return await ApplicationDbContext.Books
                                            .Where(b => collectionOfBookAuthors.Contains(b.BookId))
                                                .Include(a => a.BookAuthors)
                                                    .ThenInclude(ba => ba.Author)
                                                    .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks(IEnumerable<Guid> bookIds)
        {
            if (bookIds == null)
            {
                throw new ArgumentNullException(nameof(bookIds));
            }

            return await ApplicationDbContext.Books
                            .Where(b => bookIds.Contains(b.BookId))
                                .Include(b => b.BookAuthors)
                                .ThenInclude(ba => ba.Book)
                            .OrderBy(b => b.Title)
                            .ToListAsync();
        }

        public PagedList<Book> GetBooks(BooksResourceParameters booksResourceParameters)
        {
            if (booksResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(booksResourceParameters));
            }

            var collectionOfBooks = ApplicationDbContext.Books
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
                );
            }

            if (!string.IsNullOrWhiteSpace(booksResourceParameters.OrderBy))
            {
                var bookPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<BookDto, Book>();

                collectionOfBooks = collectionOfBooks.ApplySort(booksResourceParameters.OrderBy,
                    bookPropertyMappingDictionary);
            }

            return PagedList<Book>.Create(collectionOfBooks,
                booksResourceParameters.PageNumber,
                booksResourceParameters.PageSize);
        }

    }
}
