using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{

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
    [InlineData("Lord Of The Rings", 0, 0, 0)]//sırasıyla title, pageCount, authorId, genreId
    [InlineData("Lord Of The Rings", 1, 0, 0)]
    [InlineData("Lord Of The Rings", 1, 1, 0)]
    [InlineData("Lor", 0, 0, 0)]
    [InlineData("Lor", 1, 0, 0)]
    [InlineData("Lor", 1, 1, 0)]
    [InlineData("Lor", 1, 1, 1)]
    [InlineData("Lor", 0, 1, 0)]
    [InlineData("Lor", 0, 0, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int authorId, int genreId)
    {
        //arrange
        CreateBookCommand command =new CreateBookCommand(null, null);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateBookModel(){
            Title= title,
            PageCount= pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1), //geçen sene olsun, patlamamak için yaptık.
            AuthorId = authorId,
            GenreId = genreId
        };

        //act

        //bir command olusturmak zorundayız cünkü commandı validate ediyoruz.
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command); //errorlari dönelim.


        //assert
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    /*
        -DateTime.Now bir dependency olduğu için ve değişken olduğu için yukarıdaki testte dışarıdan veremiyoruz
        -çünkü testlerde değişken veriler istemiyoruz.
        -fakat PublishDate'in bugünden küçük olması gerektiği case'ini test etmedik.
        -bunun için bir case yazmalıyız.
    */

    [Fact]
    public void WhenDateTimeEqualsNowIsGiven_Validator_ShouldBeReturnError()
    {
         //arrange
        CreateBookCommand command =new CreateBookCommand(null, null);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateBookModel(){
            Title= "Lord of The Rings",
            PageCount= 500,
            PublishDate = DateTime.Now.Date,  //hata vermesini istiyoruz, bugünden küçük olmalı.
            AuthorId = 1,
            GenreId = 1
        };

        //act.
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0); //
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
        CreateBookCommand command =new CreateBookCommand(null, null);//Validator yapımız commandı validate ediyor ama constructor kullanmıyor.
        command.Model = new CreateBookModel(){
            Title= "Lord of The Rings",
            PageCount= 500,
            PublishDate = DateTime.Now.Date.AddYears(-2),  
            AuthorId = 1,
            GenreId = 1
        };

        //act.
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeLessThanOrEqualTo(0);
    }

}

