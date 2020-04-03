using AutoMapper;
using Library.Application.Authors.Models;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository ??
                throw new ArgumentNullException(nameof(libraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = _libraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }

        [HttpGet("{authorId}/books")]
        public ActionResult<IEnumerable<BookDto>> GetBooksForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var booksForAuthor = _libraryRepository.GetBooks(authorId);

            //var book = _mapper.Map<IEnumerable<BookViewModel>>(booksForAuthor);

            return Ok(_mapper.Map<IEnumerable<BookViewModel>>(booksForAuthor));
        }

        [HttpGet("{authorId}/books/{bookId}", Name = "GetBookForAuthor")]
        public ActionResult<BookViewModel> GetCourseForAuthor(Guid authorId, Guid bookId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthor = _libraryRepository.GetBook(authorId, bookId);

            if (bookForAuthor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookViewModel>(bookForAuthor));
        }

        [HttpPost("{authorId}/books")]
        public ActionResult<BookDto> CreateBookForAuthor(
            Guid authorId, BookForCreationForAuthorDto book)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = _mapper.Map<Book>(book);

            var authorEntity = _libraryRepository.GetAuthor(authorId);

            _libraryRepository.AddBook(bookEntity);
            _libraryRepository.AddBookAuthor(bookEntity, authorEntity);
            _libraryRepository.Save();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBookForAuthor",
                new { authorId = authorId, bookId = bookToReturn.BookId },
                bookToReturn);
        }
    }
}
