using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly IBookStoreDBContext _context;

    private readonly IMapper _mapper;
    public UpdateGenreCommandValidatorTest(CommonTestFeature testFeature)
    {
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.

    /*
        -Happy Path, bir book objesi oluşturulursa, olması gerekenler.
    */

 
    [Theory]
    [InlineData("Mo", 0)]
    [InlineData("Mo", 0)]
    [InlineData("Mo", 1)]
    [InlineData("Mob", 0)]
    [InlineData("Mob", 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, int genreId)
    {
        
        //arrange
        UpdateGenreCommand updateCommand = new UpdateGenreCommand(_context);
        GenreUpdateModel updateModel = new GenreUpdateModel(){Name = name, isActive = false}; //update model olusturuldu.
        updateCommand.Model = updateModel;
        updateCommand.GenreId = genreId; //güncellemek istedigimiz id'yi veriyoruz.

        //act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(updateCommand);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateValidator_ShouldValidate()
    {
        //arrange
        UpdateGenreCommand updateCommand = new UpdateGenreCommand(_context);
        GenreUpdateModel updateModel = new GenreUpdateModel(){Name = "Browny Wise", isActive = false}; //update model olusturuldu.
        updateCommand.Model = updateModel;
        updateCommand.GenreId = 500; //güncellemek istedigimiz id'yi veriyoruz.
        
        //act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(updateCommand);

        //assert
        result.Errors.Count.Should().BeLessThanOrEqualTo(0);

    }

}

