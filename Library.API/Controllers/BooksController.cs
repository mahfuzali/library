using AutoMapper;
using Library.Application.Dtos.Models;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public BooksController(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository ??
                throw new ArgumentNullException(nameof(libraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public IActionResult GetBooks()
        {
            var booksFromRepo = _libraryRepository.GetBooks();
            return Ok(_mapper.Map<IEnumerable<BookDto>>(booksFromRepo));
        }

        [HttpGet("{bookId}")]
        public IActionResult GetBook(Guid bookId)
        {
            var bookFromRepo = _libraryRepository.GetBook(bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(bookFromRepo));
        }
    }
}
