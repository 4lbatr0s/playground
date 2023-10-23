using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandValidatorTest(CommonTestFeature testFeature)
    {
        _mapper = testFeature.Mapper;
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.
     
    /*
        -Happy Path, bir book objesi oluşturulursa, olması gerekenler.
    */
    [Theory]
    [InlineData("Mo", 0,0)]
    [InlineData("Mo", 1,0)]
    [InlineData("Mo", 1,1)]
    [InlineData("Moby Dick", 0,0)]
    [InlineData("Moby Dick", 1,0)]
    [InlineData("Moby Dick", 0,1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int authorId)
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel(){Title = "Peter Pan", AuthorId = 1, GenreId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-50)};
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var book = _context.Books.SingleOrDefault(b => b.Title == "Peter Pan");


        UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
        BookUpdateModel updateModel = new BookUpdateModel(){AuthorId = authorId, GenreId = genreId, Title = title}; //update model olusturuldu.
        updateCommand.Model = updateModel;
        updateCommand.BookId = book.Id; //güncellemek istedigimiz id'yi veriyoruz.
        FluentActions.Invoking(() => updateCommand.Handle()).Invoke(); //handle calistirildi, guncelleme olusturuldu.
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(updateCommand);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateValidator_ShouldValidate()
    {
        //arrange
        CreateBookCommand command =new CreateBookCommand(_context, _mapper);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateBookModel(){
            Title= "1001 Tales",
            PageCount= 500,
            PublishDate = DateTime.Now.Date.AddYears(-1), //geçen sene olsun, patlamamak için yaptık.
            AuthorId = 1,
            GenreId = 1
        };

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var book = _context.Books.SingleOrDefault(b => b.Title == "1001 Tales");

        UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
        updateCommand.Model = new BookUpdateModel(){AuthorId = 1, GenreId = 1, Title = "The Great Gatsby"}; //update model olusturuldu.
        updateCommand.BookId = book.Id; //güncellemek istedigimiz id'yi veriyoruz.
        FluentActions.Invoking(() => updateCommand.Handle()).Invoke(); //handle calistirildi, guncelleme olusturuldu.
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(updateCommand);

        //assert
        result.Errors.Count.Should().BeLessThanOrEqualTo(0);

    }

}

