using AutoMapper;
using Library.Application.Books.ResourceParameters;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public IActionResult GetBooks(
            [FromQuery] BooksResourceParameters booksResourceParameters)
        {
            var booksFromRepo = _libraryRepository.GetBooks(booksResourceParameters);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(booksFromRepo));
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public IActionResult GetBook(Guid bookId)
        {
            var bookFromRepo = _libraryRepository.GetBook(bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(bookFromRepo));
        }

        [HttpPost]
        public ActionResult<BookDto> CreateBook(BookForCreationDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);
                        
            _libraryRepository.AddBook(bookEntity);

            foreach (var author in book.Authors)
            { 
                var authorEntity = _mapper.Map<Author>(author);
                _libraryRepository.AddAuthor(authorEntity);
                //_libraryRepository.Save();

                _libraryRepository.AddBookAuthor(bookEntity, authorEntity);
            }

            _libraryRepository.Save();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBook",
                new { bookId = bookToReturn.BookId },
                bookToReturn);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
