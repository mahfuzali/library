using Library.Application.Common.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Repositories
{
    public class BookAuthorRepository : Repository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(ApplicationDbContext context, IPropertyMappingService propertyMappingService)
        : base(context, propertyMappingService)
        {
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public void AddBookAuthor(Book book, Author author)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            BookAuthor bookAuthor = new BookAuthor
            {
                BookId = book.BookId,
                Book = book,
                AuthorId = author.AuthorId,
                Author = author
            };

            ApplicationDbContext.BookAuthors.Add(bookAuthor);
        }

    }
}
