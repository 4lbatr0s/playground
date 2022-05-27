using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
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
    [InlineData(-1)] //bookId
    [InlineData(0)]
    [InlineData(-2)]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        //arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = bookId;
        
        //act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)] //bookId
    [InlineData(2)]
    [InlineData(3)]
    public void WhenValidInputAreGiven_Validator_ShouldBeValidate(int bookId)
    {
        //arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = bookId;
        
        //act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeLessThanOrEqualTo(0);
    }
   
}

