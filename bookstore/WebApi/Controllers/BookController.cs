using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase //mvc uzayindan gelir.
    {
        private readonly BookStoreDBContext _bookStoreDbContext; // readonly : cannot be changed inside from application.
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext bookStoreDbContext, IMapper mapper)
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
            try
            {
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                var book = query.Handle();
                return Ok(book);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBookModel)
        {
            CreateBookCommand command = new CreateBookCommand(_bookStoreDbContext, _mapper);

            try
            {
                command.Model = newBookModel;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command); //validate, if there is an exception: throw.
                command.Handle();

                // if(!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         Console.WriteLine("Property: " + item.PropertyName + "Error message: " + item.ErrorMessage);
                //     }

                //     return BadRequest();
                // }
                // else{
                //     command.Handle();
                // }
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
           
        }

         [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookUpdateModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_bookStoreDbContext);
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            var book = _bookStoreDbContext.Books.Find(command.BookId);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(DeleteBookModel bookModel)
        {
            DeleteBookCommand command = new DeleteBookCommand(_bookStoreDbContext);
            try
            {
                command.Model = bookModel;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }


    }
}