using AutoMapper;
using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public BooksController(ILibraryRepository libraryRepository, 
            IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            _libraryRepository = libraryRepository ??
                throw new ArgumentNullException(nameof(libraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = "GetBooks")]
        [HttpHead]
        public IActionResult GetBooks(
            [FromQuery] BooksResourceParameters booksResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<BookDto, Book>
                (booksResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<BookDto>
                (booksResourceParameters.Fields))
            {
                return BadRequest();
            }

            var booksFromRepo = _libraryRepository.GetBooks(booksResourceParameters);

            var previousPageLink = booksFromRepo.HasPrevious ?
                CreateBooksResourceUri(booksResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = booksFromRepo.HasNext ?
                CreateBooksResourceUri(booksResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = booksFromRepo.TotalCount,
                pageSize = booksFromRepo.PageSize,
                currentPage = booksFromRepo.CurrentPage,
                totalPages = booksFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<BookDto>>(booksFromRepo)
                .ShapeData(booksResourceParameters.Fields));
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var bookFromRepo = await _libraryRepository.GetBook(bookId);

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

            _libraryRepository.SaveAsync();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBook",
                new { bookId = bookToReturn.BookId },
                bookToReturn);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(Guid bookId, BookForUpdateDto book)
        {
            bool checkBookExists = await _libraryRepository.BookExists(bookId);

            if (!checkBookExists)
            {
                return NotFound();
            }

            var bookEntity = await _libraryRepository.GetBook(bookId);

            if (bookEntity == null)
            { 
                var bookToAdd = _mapper.Map<Book>(book);

                bookToAdd.BookId = bookId;

                _libraryRepository.AddBook(bookToAdd);

                foreach (var author in book.Authors)
                {
                    var authorEntity = _mapper.Map<Author>(author);
                    _libraryRepository.AddAuthor(authorEntity);

                    _libraryRepository.AddBookAuthor(bookToAdd, authorEntity);
                }

                await _libraryRepository.SaveAsync();

                var bookToReturn = _mapper.Map<BookDto>(bookToAdd);
                return CreatedAtRoute("GetBook",
                    new { bookId = bookToReturn.BookId },
                    bookToReturn);
            }

            _mapper.Map(book, bookEntity);
            _libraryRepository.UpdateBook(bookEntity);
            
            await _libraryRepository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            bool checkBookExists = await _libraryRepository.BookExists(bookId);

            if (!checkBookExists)
            {
                return NotFound();
            }

            var bookFromRepo = await _libraryRepository.GetBook(bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteBook(bookFromRepo);
            await _libraryRepository.SaveAsync();

            return NoContent();
        }

        private string CreateBooksResourceUri(
           BooksResourceParameters booksResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetBooks",
                      new
                      {
                          fields = booksResourceParameters.Fields,
                          orderBy = booksResourceParameters.OrderBy,
                          pageNumber = booksResourceParameters.PageNumber - 1,
                          pageSize = booksResourceParameters.PageSize,
                          title = booksResourceParameters.Title,
                          searchQuery = booksResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetBooks",
                      new
                      {
                          fields = booksResourceParameters.Fields,
                          orderBy = booksResourceParameters.OrderBy,
                          pageNumber = booksResourceParameters.PageNumber + 1,
                          pageSize = booksResourceParameters.PageSize,
                          title = booksResourceParameters.Title,
                          searchQuery = booksResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetBooks",
                    new
                    {
                        fields = booksResourceParameters.Fields,
                        orderBy = booksResourceParameters.OrderBy,
                        pageNumber = booksResourceParameters.PageNumber,
                        pageSize = booksResourceParameters.PageSize,
                        title = booksResourceParameters.Title,
                        searchQuery = booksResourceParameters.SearchQuery
                    });
            }

        }
    }
}
