using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandTest(CommonTestFeature testFeature)
    {
        _mapper = testFeature.Mapper;
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.
    
    [Fact] //bu bir facttir, bu bir testtir.
    public void WhenRequestedBookDoesNotExists_InvalidOPerarationException_ShouldBeReturn()
    { 
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "My Father's Paradise", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;
        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);


        UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
        BookUpdateModel updateModel = new BookUpdateModel(){AuthorId = 2, GenreId = 2, Title = "Moby Dick"}; //update model olusturuldu.
        updateCommand.Model = updateModel;
        updateCommand.BookId = 1000; //sahte bir id veriyoruz.


        //act - calistirma
        FluentActions
            .Invoking(() => updateCommand.Handle()) //handle calistirildi, Model'den yeni bir book calistirildi. //act kismi
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadi"); //assert kismi

        //assert - dogrulama

    }

    /*
        -Happy Path, bir book objesi oluşturulursa, olması gerekenler.
    */
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "Bridge of Fortune", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);


        UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
        BookUpdateModel updateModel = new BookUpdateModel(){AuthorId = 2, GenreId = 2, Title = "Kalahari Desert"}; //update model olusturuldu.
        updateCommand.Model = updateModel;
        updateCommand.BookId = book.Id; //güncellemek istedigimiz id'yi veriyoruz.


        //act - calistirma
        FluentActions.Invoking(() => updateCommand.Handle()).Invoke(); //handle calistirildi, guncelleme olusturuldu.
        var updatedBook = _context.Books.SingleOrDefault(b => b.Id == book.Id);
        
        //assert - dogrulama
        updatedBook.AuthorId.Should().Be(updateModel.AuthorId); //güncellendi
        updatedBook.GenreId.Should().Be(updateModel.GenreId);
        updatedBook.Title.Should().Be(updateModel.Title);
    }
}

