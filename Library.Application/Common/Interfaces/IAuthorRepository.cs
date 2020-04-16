using Library.Application.Authors.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<Author> GetAuthor(Guid authorId);
        
        Task<Author> GetAuthor(Guid authorId, Guid bookId);
        
        Task<IEnumerable<Author>> GetAuthors();
        
        Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds);
        
        PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);

        void AddAuthor(Author author);
        
        void DeleteAuthor(Author author);
        
        void UpdateAuthor(Author author);
        
        Task<bool> AuthorExists(Guid authorId);
        
        Task<bool> AuthorExists(string firstName, string lastName, DateTimeOffset dateOfBirth);
    }
}
