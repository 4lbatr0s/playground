using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.DeleteGenre;


public class DeleteGenreCommandTest:IClassFixture<CommonTestFeature>
{
    //deletebookcommand sadece context kullanıyor, mappere gerek yok.
    private readonly IBookStoreDBContext _bookStoreDbContext;
    private readonly IMapper _mapper;

    public DeleteGenreCommandTest(CommonTestFeature commonTestFeature)
    {
        _bookStoreDbContext = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIdIsInvalid_InvalidOperationException_ShouldBeReturn()
        {
          //arrange
          DeleteGenreCommand command = new DeleteGenreCommand(_bookStoreDbContext);
          command.GenreId = 5000;

          //act and assert
          FluentActions.Invoking(() => command.Handle()) //delete the genre with the id of 5000;
          .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre mevcut degil!");
    
    }



    /*
        Happy Path..
    */
    [Fact]
    public void WhenGivenGenreIdIsValid_Genre_ShouldBeDeletedFromDatabase()
    {
          //arrange
          DeleteGenreCommand command = new DeleteGenreCommand(_bookStoreDbContext);
          command.GenreId = 3;

          //act 
          FluentActions.Invoking(() => command.Handle()).Invoke(); //delete the genre with the id of 5000;
          var genre = _bookStoreDbContext.Genres.SingleOrDefault(c => c.Id == command.GenreId);

          //ASSERT
          genre.Should().BeNull();
    }

}