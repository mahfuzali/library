using AutoMapper;
using Library.Application.Common.Helpers;
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
    [Route("api/bookcollections")]
    public class BookCollectionsController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public BookCollectionsController(ILibraryRepository libraryRepository,
            IMapper mapper)
        {
            _libraryRepository = libraryRepository ??
                throw new ArgumentNullException(nameof(libraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetBookCollection")]
        public IActionResult GetBookCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var bookEntities = _libraryRepository.GetBooks(ids);

            if (ids.Count() != bookEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<BookDto>>(bookEntities);

            return Ok(authorsToReturn);
        }


        [HttpPost]
        public ActionResult<IEnumerable<BookDto>> CreateAuthorCollection(
            IEnumerable<BookForCreationDto> bookCollection)
        {
            /*
            var bookEntities = _mapper.Map<IEnumerable<Book>>(bookCollection);
            foreach (var book in bookEntities)
            {
                _libraryRepository.AddBook(book);
            }

            _libraryRepository.Save();

            var bookCollectionToReturn = _mapper.Map<IEnumerable<BookDto>>(bookEntities);
            var idsAsString = string.Join(",", bookCollectionToReturn.Select(b => b.BookId));
            return CreatedAtRoute("GetBookCollection",
             new { ids = idsAsString },
             bookCollectionToReturn);
             */

            IList<Book> books = new List<Book>();

            foreach (var book in bookCollection) 
            {
                var bookEntity = _mapper.Map<Book>(book);
                books.Add(bookEntity);

                _libraryRepository.AddBook(bookEntity);
                foreach (var author in book.Authors)
                {
                    var authorEntity = _mapper.Map<Author>(author);

                    _libraryRepository.AddAuthor(authorEntity);

                    _libraryRepository.AddBookAuthor(bookEntity, authorEntity);
                }

                _libraryRepository.Save();
            }

            var bookCollectionToReturn = _mapper.Map<IEnumerable<BookDto>>(books.AsEnumerable());
            var idsAsString = string.Join(",", bookCollectionToReturn.Select(b => b.BookId));
            return CreatedAtRoute("GetBookCollection",
             new { ids = idsAsString },
             bookCollectionToReturn);
        }
    }
}
