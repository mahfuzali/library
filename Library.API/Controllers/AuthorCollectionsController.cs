using AutoMapper;
using Library.Application.Authors.Models;
using Library.Application.Common.Helpers;
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
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ILibraryRepository libraryRepository,
            IMapper mapper)
        {
            _libraryRepository = libraryRepository ??
                throw new ArgumentNullException(nameof(libraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public async Task<IActionResult> GetAuthorCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = await _libraryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            return Ok(authorsToReturn);
        }
    }
}
