using Library.Application.Common.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public RepositoryWrapper(ApplicationDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context;
            Books = new BookRepository(_context, _propertyMappingService);
            Authors = new AuthorRepository(_context, _propertyMappingService);
        }

        public IAuthorRepository Authors { get; private set; }

        public IBookRepository Books { get; private set; }

        public IBookAuthorRepository BookAuthors { get; private set; }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
