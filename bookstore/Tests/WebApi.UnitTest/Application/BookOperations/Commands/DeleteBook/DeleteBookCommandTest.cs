using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.DeleteBook;


public class DeleteBookCommandTest:IClassFixture<CommonTestFeature>
{
    //deletebookcommand sadece context kullanıyor, mappere gerek yok.
    private readonly IBookStoreDBContext _bookStoreDbContext;
    private readonly IMapper _mapper;

    public DeleteBookCommandTest(CommonTestFeature commonTestFeature)
    {
        _bookStoreDbContext = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }

    [Fact]
    public void WhenGivenBookIdIsInvalid_InvalidOperationException_ShouldBeReturn()
        {
          //arrange
        CreateBookCommand command = new CreateBookCommand(_bookStoreDbContext, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "Alchemist", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;
        

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _bookStoreDbContext.Add() fonksiyonunu calistiriyor.
        var book = _bookStoreDbContext.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);

        DeleteBookCommand deleteCommand = new DeleteBookCommand(_bookStoreDbContext);
        deleteCommand.BookId = 1000;
        
        //assert-act
        FluentActions
        .Invoking(() => deleteCommand.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");
        book.Should().NotBeNull();
    
    }

    /*
        Happy Path..
    */
    [Fact]
    public void WhenGivenBookIdIsValid_Book_ShouldBeDeletedFromDatabase()
    {
          //arrange
        CreateBookCommand command = new CreateBookCommand(_bookStoreDbContext, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "1984", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;
        

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _bookStoreDbContext.Add() fonksiyonunu calistiriyor.
        var book = _bookStoreDbContext.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);

        DeleteBookCommand deleteCommand = new DeleteBookCommand(_bookStoreDbContext);
        deleteCommand.BookId = book.Id; //az önce olusturdugumuz kitabın idsini verdik.
        
        FluentActions
        .Invoking(() => deleteCommand.Handle()).Invoke();
    
        //assert
        var deletedBook = _bookStoreDbContext.Books.SingleOrDefault(b => b.Title == book.Title);
        deletedBook.Should().BeNull();
    }

}