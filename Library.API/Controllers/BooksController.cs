using AutoMapper;
using Library.Application.Books.ResourceParameters;
using Library.Application.Common.Helpers;
using Library.Application.Common.Interfaces;
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
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        private readonly IRepositoryWrapper _repositoryWrapper;

        public BooksController(IMapper mapper, IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService,
            IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));

            _repositoryWrapper = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
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

            var booksFromRepo = _repositoryWrapper.Books.GetBooks(booksResourceParameters);

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
        public async Task<IActionResult> GetBookAsync(Guid bookId)
        {
            var bookFromRepo = await _repositoryWrapper.Books.GetBook(bookId);

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

            if (!TryValidateModel(bookEntity))
            {
                return ValidationProblem(ModelState);
            }

            _repositoryWrapper.Books.Add(bookEntity);

            foreach (var author in book.Authors)
            {
                var authorEntity = _mapper.Map<Author>(author);
                _repositoryWrapper.Authors.Add(authorEntity);

                BookAuthor bookAuthorEntity = new BookAuthor()
                {
                    BookId = bookEntity.BookId,
                    Book = bookEntity,
                    AuthorId = authorEntity.AuthorId,
                    Author = authorEntity
                };
                _repositoryWrapper.BookAuthors.Add(bookAuthorEntity);
            }

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBook",
                new { bookId = bookToReturn.BookId },
                bookToReturn);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(Guid bookId, BookForUpdateDto book)
        {
            //Book checkBookExists = _repositoryWrapper.Books.Get(bookId);

            //if (checkBookExists == null)
            //{
            //    return NotFound();
            //}

            var bookEntity = await _repositoryWrapper.Books.GetBook(bookId);

            if (!TryValidateModel(bookEntity))
            {
                return ValidationProblem(ModelState);
            }

            if (bookEntity == null)
            {
                var bookToAdd = _mapper.Map<Book>(book);

                bookToAdd.BookId = bookId;

                _repositoryWrapper.Books.Add(bookToAdd);

                foreach (var author in book.Authors)
                {
                    var authorEntity = _mapper.Map<Author>(author);
                    _repositoryWrapper.Authors.Add(authorEntity);

                    BookAuthor bookAuthor = new BookAuthor()
                    {
                        BookId = bookToAdd.BookId,
                        Book = bookToAdd,
                        AuthorId = authorEntity.AuthorId,
                        Author = authorEntity
                    };

                    _repositoryWrapper.BookAuthors.Add(bookAuthor);
                }

                var bookToReturn = _mapper.Map<BookDto>(bookToAdd);
                return CreatedAtRoute("GetBook",
                    new { bookId = bookToReturn.BookId },
                    bookToReturn);
            }

            _mapper.Map(book, bookEntity);
            _repositoryWrapper.Books.Update(bookEntity);

            foreach (var author in book.Authors)
            {
                var authorEntity = _mapper.Map<Author>(author);
                _repositoryWrapper.Authors.Update(authorEntity);
            }

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        /// <summary>
        /// Deletes a specific Book.
        /// </summary>
        /// <param name="bookId"></param>  
        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(Guid bookId)
        {
            Book checkBookExists = _repositoryWrapper.Books.Get(bookId);

            if (checkBookExists == null)
            {
                return NotFound();
            }

            _repositoryWrapper.Books.Remove(checkBookExists);

            return NoContent();
        }


        [HttpGet("collection/({ids})", Name = "GetBookCollection")]
        public async Task<IActionResult> GetBookCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var bookEntities = await _repositoryWrapper.Books.GetBooks(ids);

            if (ids.Count() != bookEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<BookDto>>(bookEntities);

            return Ok(authorsToReturn);
        }


        [HttpPost("collection")]
        public ActionResult<IEnumerable<BookDto>> CreateBookCollection(
            IEnumerable<BookForCreationDto> bookCollection)
        {
            //IList<Book> books = new List<Book>();

            //foreach (var book in bookCollection)
            //{
            //    var bookEntity = _mapper.Map<Book>(book);
            //    books.Add(bookEntity);

            //    _repositoryWrapper.Books.Add(bookEntity);

            //    foreach (var author in book.Authors)
            //    {
            //        var authorEntity = _mapper.Map<Author>(author);

            //        _repositoryWrapper.Authors.Add(authorEntity);

            //        BookAuthor bookAuthor = new BookAuthor()
            //        {
            //            BookId = bookEntity.BookId,
            //            Book = bookEntity,
            //            AuthorId = authorEntity.AuthorId,
            //            Author = authorEntity
            //        };

            //        _repositoryWrapper.BookAuthors.Add(bookAuthor);
            //    }
            //}

            //var bookCollectionToReturn = _mapper.Map<IEnumerable<BookDto>>(books.AsEnumerable());
            //var idsAsString = string.Join(",", bookCollectionToReturn.Select(b => b.BookId));
            //return CreatedAtRoute("GetBookCollection",
            // new { ids = idsAsString },
            // bookCollectionToReturn);


            IList<Book> books = new List<Book>();

            foreach (var book in bookCollection)
            {
                var bookEntity = _mapper.Map<Book>(book);
                _repositoryWrapper.Books.Add(bookEntity);
                books.Add(bookEntity);

                foreach (var author in book.Authors)
                {
                    var authorEntity = _mapper.Map<Author>(author);
                    _repositoryWrapper.Authors.Add(authorEntity);
                    
                    BookAuthor bookAuthor = new BookAuthor()
                    {
                        BookId = bookEntity.BookId,
                        Book = bookEntity,
                        AuthorId = authorEntity.AuthorId,
                        Author = authorEntity
                    };

                    _repositoryWrapper.BookAuthors.Add(bookAuthor);
                }
            }

            var bookCollectionToReturn = _mapper.Map<IEnumerable<BookDto>>(books.AsEnumerable());
            var idsAsString = string.Join(",", books);
            return CreatedAtRoute("GetBookCollection",
             new { ids = idsAsString }, bookCollectionToReturn);
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
