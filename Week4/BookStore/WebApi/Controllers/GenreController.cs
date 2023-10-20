using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Select Genre By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GetGenreDetailQueryValidator validation = new GetGenreDetailQueryValidator();
            query.Id = id;
            validation.ValidateAndThrow(query);
            GenreViewModel vm = query.Handle();
            return Ok(vm);
        }

        //Select Genres
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            List<GenresViewModel> vm = query.Handle();
            return Ok(vm);
        }

        //Create Genre
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            command.Model = newGenre;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        //Update Genre
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            command.Id = id;
            command.Model = updatedGenre;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();    
        }

        //Delete Genre
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            command.Id = id;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}