using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Querys;
using WebApi.Application.GenreOperations.Validator;
using WebApi.DbOprations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // Tüm türleri listeler.
        [HttpGet]
        public IActionResult GetListGenres()
        {
            GetListGenreQuery query = new GetListGenreQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }

        // Belirli bir türün bilgilerini getirir.
        [HttpGet("{id}")]
        public IActionResult GetByIdGenre(int id)
        {
            GenreDetailModel result;

            GetByIdGenreQuery query = new GetByIdGenreQuery(_context, _mapper);
            query.GenreId = id;

            result = query.Handle();

            return Ok(result);
        }

        // Yeni bir tür ekler.
        [HttpPost]
        public IActionResult Add([FromBody] CreateGenreModel model)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        // Belirli bir türü siler.
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreID = id;

            command.Handle();
            return Ok();

        }

        // Belirli bir türün bilgilerini günceller.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateGenreModel model)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);

            command.GenreID = id;

            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

    }
}
