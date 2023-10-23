using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly IBookStoreDBContext _context;
    public UpdateGenreCommandTest(CommonTestFeature testFeature)
    {
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.
    
    [Fact] //bu bir facttir, bu bir testtir.
    public void WhenRequestedGenreDoesNotExists_InvalidOPerarationException_ShouldBeReturn()
    { 
      
        UpdateGenreCommand updateCommand = new UpdateGenreCommand(_context);
        GenreUpdateModel updateModel = new GenreUpdateModel(){isActive= false, Name = "Musical Reality"};
        updateCommand.Model = updateModel;
        updateCommand.GenreId = 1000; //sahte bir id veriyoruz.


        //act - calistirma  - assert
        FluentActions
            .Invoking(() => updateCommand.Handle()) //handle calistirildi, Model'den yeni bir book calistirildi. //act kismi
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre mevcut degil!"); //assert kismi


    }

    /*
        -Happy Path, bir book objesi oluşturulursa, olması gerekenler.
    */
   [Fact] //bu bir facttir, bu bir testtir.
    public void WhenRequestedGenreDoesExists_Genre_ShouldBeUpdated()
    { 
      
        UpdateGenreCommand updateCommand = new UpdateGenreCommand(_context);
        GenreUpdateModel updateModel = new GenreUpdateModel(){isActive= false, Name = "Lyrical Reality"};
        updateCommand.Model = updateModel;
        updateCommand.GenreId = 2; //sahte bir id veriyoruz.
        

        //act - calistirma
        FluentActions.Invoking(() => updateCommand.Handle()).Invoke();

        //assert - dogrulama
        var genre = _context.Genres.SingleOrDefault(genre => genre.Id == updateCommand.GenreId);

        genre.Should().NotBeNull();
        genre.Id.Should().Be(2);
        genre.Name.Should().Be("Lyrical Reality");
        

    }
}

