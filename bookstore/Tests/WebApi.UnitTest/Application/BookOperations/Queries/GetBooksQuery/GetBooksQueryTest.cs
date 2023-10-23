using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace Application.BookOperations.Queries;


public class GetBooksQueryTest:IClassFixture<CommonTestFeature>
{
    private readonly IBookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetBooksQueryTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }

    CreateBookModel firstBook = new CreateBookModel(){Title = "Prince Metrub", AuthorId = 3, PageCount = 3000, GenreId = 3, PublishDate = DateTime.Now.AddYears(-50)};
    CreateBookModel secondBook = new CreateBookModel(){Title = "Prince Perub", AuthorId = 3, PageCount = 3000, GenreId = 3, PublishDate = DateTime.Now.AddYears(-50)};

    [Fact]
    public void WhenThereIsNoBooks_Books_ShouldNotBeReturned()
    {
        GetBooksQuery command = new GetBooksQuery(_context, _mapper);
        
        //act - calistirma
        FluentActions.Invoking(() => command.Handle()).Invoke(); //handle calistirildi, Model'den yeni bir book calistirildi. //act kismi
        var books = _context.Books.ToList();
        //assert - dogrulama
        books.Should().BeNullOrEmpty();

    }
  

    [Fact]
    public void WhenThereIsBooks_Books_ShouldBeReturned(){
          
          //arrange
          CreateBookCommand command = new CreateBookCommand(_context, _mapper);
          command.Model = firstBook;
          FluentActions.Invoking(() => command.Handle()).Invoke();
          command.Model = secondBook;
          FluentActions.Invoking(() => command.Handle()).Invoke();

          //act
          GetBooksQuery query = new GetBooksQuery(_context,_mapper);
          FluentActions.Invoking(() => query.Handle()) //act and assert.
          .Should().NotBeNull();
          

         //assert. 
          var books = _context.Books.ToList();
          books.Count().Should().Be(2);

          
    }
}