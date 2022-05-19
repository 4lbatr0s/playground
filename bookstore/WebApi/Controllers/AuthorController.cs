using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Application.AuthorOperations;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.authorOperations.Commands.DeleteAuthor;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController:ControllerBase //mvc uzayindan gelir.
    {
        private readonly BookStoreDBContext _bookStoreDbContext; // readonly : cannot be changed inside from application.
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDBContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_bookStoreDbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


                
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
                GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_bookStoreDbContext, _mapper);
                query.AuthorId = id;
                GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
                validator.ValidateAndThrow(query);
                var author = query.Handle();
                return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthorModel)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_bookStoreDbContext, _mapper);
            command.Model = newAuthorModel;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command); //validate, if there is an exception: throw.
            command.Handle();
            return Ok();
           
        }

         [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorUpdateModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_bookStoreDbContext);
            command.AuthorId = id;
            command.Model = updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            var book = _bookStoreDbContext.Books.Find(command.AuthorId);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteBook(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_bookStoreDbContext);
            command.authorId= authorId;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


    }
}