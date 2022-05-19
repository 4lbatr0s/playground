using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Application.GenreOperations;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Controllers;

    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase //mvc uzayindan gelir.
    {
        private readonly BookStoreDBContext _bookStoreDBContext;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDBContext bookStoreDBContext, IMapper mapper)
        {
            _bookStoreDBContext = bookStoreDBContext;
            _mapper = mapper;
        }

            [HttpGet]
            public IActionResult GetGenres()
            {
                GetGenresQuery query = new GetGenresQuery(_bookStoreDBContext, _mapper);
                var result = query.Handle();
                return Ok(result);
            }


                    
            [HttpGet("{id}")]
            public IActionResult GetGenreById(int id)
            {
                    GetGenreDetailsQuery query = new GetGenreDetailsQuery(_bookStoreDBContext, _mapper);
                    query.GenreId = id;//GenreId = id in order to make GetGenreDetailsQuery class to work.
                    GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
                    validator.ValidateAndThrow(query);
                    var genre = query.Handle();
                    return Ok(genre);
            }

            [HttpPost]
            public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
            {
                CreateGenreCommand command = new CreateGenreCommand(_mapper, _bookStoreDBContext);
                command.Model = newGenre;
                CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
                validator.ValidateAndThrow(command); //validate, if there is an exception: throw.
                command.Handle();
                return Ok();
            
            }

            [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, [FromBody] GenreUpdateModel updatedGenre)
            {
                UpdateGenreCommand command = new UpdateGenreCommand(_bookStoreDBContext);
                command.GenreId= id;
                command.Model = updatedGenre;
                UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
                var book = _bookStoreDBContext.Books.Find(command.GenreId);
                return Ok(book);
            }

            [HttpDelete]
            public IActionResult DeleteBook(int genreId)
            {
                DeleteGenreCommand command = new DeleteGenreCommand(_bookStoreDBContext);
            
                command.GenreId = genreId;
                DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
    }
