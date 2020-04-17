using AutoMapper;
using Library.Application.Authors.Models;
using Library.Application.Authors.ResourceParameters;
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
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    [Authorize]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    //[HttpCacheValidation(MustRevalidate = true)]
    public class AuthorsController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public AuthorsController(ILibraryRepository libraryRepository, 
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

            var authorsFromRepo = _libraryRepository.GetAuthors(authorsResourceParameters);

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
        public async Task<IActionResult> GetAuthor(Guid authorId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<AuthorDto>
                (fields))
            {
                return BadRequest();
            }

            var authorFromRepo = await _libraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo).ShapeData(fields));
        }

        [HttpGet("{authorId}/books")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooksForAuthor(Guid authorId)
        {
            bool checkAuthorExists = await _libraryRepository.AuthorExists(authorId);
            if (!checkAuthorExists)
            {
                return NotFound();
            }

            var booksForAuthor = await _libraryRepository.GetBooks(authorId);

            return Ok(_mapper.Map<IEnumerable<BookViewModel>>(booksForAuthor));
        }

        [HttpGet("{authorId}/books/{bookId}", Name = "GetBookForAuthor")]
        public async Task<ActionResult<BookViewModel>> GetCourseForAuthor(Guid authorId, Guid bookId)
        {
            bool checkAuthorExists = await _libraryRepository.AuthorExists(authorId);

            if (!checkAuthorExists)
            {
                return NotFound();
            }

            var bookForAuthor = await _libraryRepository.GetBook(authorId, bookId);

            if (bookForAuthor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookViewModel>(bookForAuthor));
        }

        [HttpPost("{authorId}/books")]
        public async Task<ActionResult<BookDto>> CreateBookForAuthor(
            Guid authorId, BookForCreationForAuthorDto book)
        {
            bool checkAuthorExists = await _libraryRepository.AuthorExists(authorId);

            if (!checkAuthorExists)
            {
                return NotFound();
            }

            var bookEntity = _mapper.Map<Book>(book);

            var authorEntity = await _libraryRepository.GetAuthor(authorId);

            _libraryRepository.AddBook(bookEntity);
            _libraryRepository.AddBookAuthor(bookEntity, authorEntity);
            await _libraryRepository.SaveAsync();

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
            bool checkAuthorExists = await _libraryRepository.AuthorExists(authorId);

            if (!checkAuthorExists)
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = await _libraryRepository.GetBook(authorId, bookId);

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

                _libraryRepository.AddBook(authorId, bookToAdd);
                await _libraryRepository.SaveAsync();

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

            _libraryRepository.UpdateBook(bookForAuthorFromRepo);

            await _libraryRepository.SaveAsync();

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
        public async Task<ActionResult> DeleteBook(Guid authorId)
        {
            bool checkAuthorExists = await _libraryRepository.AuthorExists(authorId);

            if (!checkAuthorExists)
            {
                return NotFound();
            }

            var authorFromRepo = await _libraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteAuthor(authorFromRepo);
            await _libraryRepository.SaveAsync();

            return NoContent();
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
