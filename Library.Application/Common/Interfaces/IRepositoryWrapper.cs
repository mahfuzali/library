using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IRepositoryWrapper : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        IBookAuthorRepository BookAuthors { get; }
        Task<int> Save();
    }
}
