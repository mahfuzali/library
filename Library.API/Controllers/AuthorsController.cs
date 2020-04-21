using AutoMapper;
using Library.Application.Authors.Models;
using Library.Application.Authors.ResourceParameters;
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
    [Route("api/authors")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        private readonly IRepositoryWrapper _repositoryWrapper;

        public AuthorsController(IMapper mapper, IPropertyMappingService propertyMappingService,
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

        [HttpGet(Name = "GetAuthors")]
        [HttpHead]
        public IActionResult GetAuthors(
            [FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<AuthorDto, Author>
                (authorsResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<AuthorDto>
                (authorsResourceParameters.Fields))
            {
                return BadRequest();
            }

            var authorsFromRepo = _repositoryWrapper.Authors.GetAuthors(authorsResourceParameters);

            var previousPageLink = authorsFromRepo.HasPrevious ?
                CreateAuthorsResourceUri(authorsResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = authorsFromRepo.HasNext ?
                CreateAuthorsResourceUri(authorsResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = authorsFromRepo.TotalCount,
                pageSize = authorsFromRepo.PageSize,
                currentPage = authorsFromRepo.CurrentPage,
                totalPages = authorsFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo)
                        .ShapeData(authorsResourceParameters.Fields));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<AuthorDto>
                (fields))
            {
                return BadRequest();
            }

            var authorFromRepo = _repositoryWrapper.Authors.Get(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo).ShapeData(fields));
        }

        [HttpGet("{authorId}/books")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooksForAuthor(Guid authorId)
        {
            var checkAuthorExists = _repositoryWrapper.Authors.Get(authorId);

            if (checkAuthorExists == null)
            {
                return NotFound();
            }

            var booksOfAnAuthor = await _repositoryWrapper.Books.GetBooksOfAnAuthor(authorId);

            return Ok(_mapper.Map<IEnumerable<BookViewModel>>(booksOfAnAuthor));
        }

        [HttpGet("{authorId}/books/{bookId}", Name = "GetBookForAuthor")]
        public async Task<ActionResult<BookViewModel>> GetCourseForAuthor(Guid authorId, Guid bookId)
        {

            var bookByAnAuthor = await _repositoryWrapper.Books.GetABookByAnAuthor(authorId, bookId);

            if (bookByAnAuthor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookViewModel>(bookByAnAuthor));
        }

        [HttpPost("{authorId}/books")]
        public ActionResult<BookDto> CreateBookForAuthor(
            Guid authorId, BookForCreationForAuthorDto book)
        {

            var checkAuthorExists = _repositoryWrapper.Authors.Get(authorId);

            if (checkAuthorExists == null)
            {
                return NotFound();
            }

            var bookEntity = _mapper.Map<Book>(book);

            var authorEntity = _repositoryWrapper.Authors.Get(authorId);

            _repositoryWrapper.Books.Add(bookEntity);

            BookAuthor bookAuthor = new BookAuthor()
            {
                BookId = bookEntity.BookId,
                Book = bookEntity,
                AuthorId = authorEntity.AuthorId,
                Author = authorEntity
            };

            _repositoryWrapper.BookAuthors.Add(bookAuthor);

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBookForAuthor",
                new { authorId = authorId, bookId = bookToReturn.BookId },
                bookToReturn);
        }


        [HttpPatch("{authorId}/books/{bookId}")]
        public async Task<IActionResult> PartiallyUpdateCourseForAuthor(Guid authorId,
            Guid bookId,
            JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var authorFromRepo = _repositoryWrapper.Authors.Get(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = await _repositoryWrapper.Books.GetABookByAnAuthor(authorId, bookId);

            if (bookForAuthorFromRepo == null)
            {
                var bookDto = new BookForUpdateDto();
                patchDocument.ApplyTo(bookDto);

                if (!TryValidateModel(bookDto))
                {
                    return ValidationProblem(ModelState);
                }

                var bookToAdd = _mapper.Map<Book>(bookDto);
                bookToAdd.BookId = bookId;

                _repositoryWrapper.Books.Add(bookToAdd);

                BookAuthor bookAuthor = new BookAuthor()
                {
                    BookId = bookToAdd.BookId,
                    Book = bookToAdd,
                    AuthorId = authorFromRepo.AuthorId,
                    Author = authorFromRepo
                };

                _repositoryWrapper.BookAuthors.Add(bookAuthor);

                var bookToReturn = _mapper.Map<BookDto>(bookToAdd);

                return CreatedAtRoute("GetBook",
                    new { bookId = bookToReturn.BookId },
                    bookToReturn);
            }

            var bookToPatch = _mapper.Map<BookForUpdateDto>(bookForAuthorFromRepo);
            // add validation
            patchDocument.ApplyTo(bookToPatch);

            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, bookForAuthorFromRepo);

            _repositoryWrapper.Books.Update(bookForAuthorFromRepo);

            return NoContent();
        }

        public override ActionResult ValidationProblem(
        [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        [HttpDelete("{authorId}")]
        public ActionResult DeleteBook(Guid authorId)
        {

            var authorFromRepo = _repositoryWrapper.Authors.Get(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            _repositoryWrapper.Authors.Remove(authorFromRepo);

            return NoContent();
        }


        [HttpGet("collection/({ids})", Name = "GetAuthorCollection")]
        public async Task<IActionResult> GetAuthorCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = await _repositoryWrapper.Authors.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            return Ok(authorsToReturn);
        }

        private string CreateAuthorsResourceUri(
           AuthorsResourceParameters authorsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          pageNumber = authorsResourceParameters.PageNumber - 1,
                          pageSize = authorsResourceParameters.PageSize,
                          name = authorsResourceParameters.Name,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          pageNumber = authorsResourceParameters.PageNumber + 1,
                          pageSize = authorsResourceParameters.PageSize,
                          name = authorsResourceParameters.Name,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetAuthors",
                    new
                    {
                        fields = authorsResourceParameters.Fields,
                        orderBy = authorsResourceParameters.OrderBy,
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize,
                        name = authorsResourceParameters.Name,
                        searchQuery = authorsResourceParameters.SearchQuery
                    });
            }
        }
    }
}
