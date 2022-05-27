using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{


    private readonly IBookStoreDBContext _context;

    public CreateGenreCommandValidatorTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
    }
    //theory nedir? 
    //Aşağıdaki testte validationları test ediyoruz.
    /*
        -title uyabilir, diğerleri uymayabilir,
        -title ve genreId uyabilir diğerleri uymayabilir
        -title uyabilir .....
        ...
        ... devam etsin.
        Her durum için ayrı test yazmak yerine, Theory ve InlineData verip, 
        inline data verileri için testi birden çok kez çalıştırıyoruz ve birden çok veri için testi geçiyor mu bakıyoruz.

    
    */
    [Theory]
    [InlineData("S")]//sırasıyla title, pageCount, authorId, genreId
    [InlineData("St")]
    [InlineData("Stu")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        //arrange
        CreateGenreCommand command =new CreateGenreCommand(null, null);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateGenreModel(){
            Name = name
        };

        //act

        //bir command olusturmak zorundayız cünkü commandı validate ediyoruz.
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command); //errorlari dönelim.


        //assert
        result.Errors.Count.Should().BeGreaterThan(0);


    }


    /*
        Sadece hatalı caseleri değil, valid caseleri de test etmek gerekiyor.
        Her şeyin düzgün yapıldığı bir case test edelim.
        Buna HAPPY PATH denir.
    */
    [Fact]
    public void WhenEveryValueIsGivenCorrectly_Validator_ShouldNotReturnErrors()
    {
         //arrange
        CreateGenreCommand command =new CreateGenreCommand(null, null);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateGenreModel(){
            Name = "Japanese Hotaku Art"
        };

        //act.
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeLessThanOrEqualTo(0);

        // var genre = _context.Genres.SingleOrDefault(g => g.Name == command.Model.Name);
        // DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
        //  deleteGenreCommand.GenreId = genre.Id;
        //  FluentActions.Invoking(() => deleteGenreCommand.Handle()).Invoke();
    }

}

