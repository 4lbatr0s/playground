using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTest:IClassFixture<CommonTestFeature> //CommonTestFeature sınıfın sağladığı context ve mapperi kullanabilmek için yaptık.
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTest(CommonTestFeature testFeature)
    {
        _mapper = testFeature.Mapper;
        _context = testFeature.Context;
    }

    //testler genelde geriye bir şey dönmez, void tipindedirler.
    
    [Fact] //bu bir facttir, bu bir testtir.
    public void WhenAlreadyExistsGenreTitleIsGiven_InvalidOPerarationException_ShouldBeReturn()
    {
        //arrange - hazırlık
        var Genre = new Genre() {Name = "Romantic Comedy"};
        _context.Genres.Add(Genre);
        _context.SaveChanges(); //veri yaratildi.



        CreateGenreCommand command = new CreateGenreCommand(_mapper,_context);
        command.Model = new CreateGenreModel(){Name = "Romantic Comedy"}; //Handle() metodu Modeli kullanarak yeni bir Genre olusturmaktadir.
        
        //act - calistirma
        FluentActions
            .Invoking(() => command.Handle()) //handle calistirildi, Model'den yeni bir Genre calistirildi. //act kismi
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre zaten mevcut!"); //assert kismi
        //assert - dogrulama

    }

    /*
        -Happy Path, bir Genre objesi oluşturulursa, olması gerekenler.
    */
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //arrange
        CreateGenreCommand command = new CreateGenreCommand( _mapper,_context);
        CreateGenreModel model = new CreateGenreModel(){Name = "Magical Reality"};
        command.Model = model;
        

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); //DB'ye bir veri ekledik. Handle zaten _context.Add() fonksiyonunu calistiriyor.
        var Genre = _context.Genres.SingleOrDefault(g => g.Name == model.Name);
        Genre.Should().NotBeNull();
        Genre.Name.Should().Be(model.Name);

        // DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
        // deleteGenreCommand.GenreId = Genre.Id;
        // FluentActions.Invoking(() => deleteGenreCommand.Handle()).Invoke();
    }
}

