using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTest(CommonTestFeature testFeature)
    {
        _mapper = testFeature.Mapper;
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.
    
    [Fact] //bu bir facttir, bu bir testtir.
    public void WhenAlreadyExistsBookTitleIsGiven_InvalidOPerarationException_ShouldBeReturn()
    {
        //arrange - hazırlık
        var book = new Book() {Title = "Gondor", PageCount = 100, PublishDate = new DateTime(1990, 1, 1), GenreId = 1, AuthorId = 1};
        _context.Books.Add(book);
        _context.SaveChanges(); //veri yaratildi.
        
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = new CreateBookModel(){Title = "Gondor"}; //Handle() metodu Modeli kullanarak yeni bir Book olusturmaktadir.
        
        //act - calistirma
        FluentActions
            .Invoking(() => command.Handle()) //handle calistirildi, Model'den yeni bir book calistirildi. //act kismi
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut"); //assert kismi
        //assert - dogrulama

    }

    /*
        -Happy Path, bir book objesi oluşturulursa, olması gerekenler.
    */
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "Hobbit", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;
        

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);
    }
}

