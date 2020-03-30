﻿using Library.Domain.Entities;
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

        public void AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool AuthorExists(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
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
