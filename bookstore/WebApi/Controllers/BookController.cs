using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.CreateBook.Commands;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase //mvc uzayindan gelir.
    {
        private readonly IBookStoreDBContext _bookStoreDbContext; // readonly : cannot be changed inside from application.
        private readonly IMapper _mapper;

        public BookController(IBookStoreDBContext bookStoreDbContext, IMapper mapper )
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_bookStoreDbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


                
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_bookStoreDbContext, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                var book = query.Handle();
                return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBookModel)
        {
            CreateBookCommand command = new CreateBookCommand(_bookStoreDbContext, _mapper);
            command.Model = newBookModel;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command); //validate, if there is an exception: throw.
            command.Handle();
            return Ok();
           
        }

         [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookUpdateModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_bookStoreDbContext);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            var book = _bookStoreDbContext.Books.Find(command.BookId);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteBook(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(_bookStoreDbContext);
            command.BookId = bookId;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


    }
}