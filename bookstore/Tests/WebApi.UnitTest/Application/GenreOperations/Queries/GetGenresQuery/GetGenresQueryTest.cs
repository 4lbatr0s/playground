
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetAllGenresQuery;



public class GetGenreQueryTest:IClassFixture<CommonTestFeature>
{
    private readonly IBookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetGenreQueryTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }


    [Fact]
    public void WhenThereIsNoGenre_Results_ShouldBeEmpty()
    {
        GetGenresQuery query = new GetGenresQuery(_context, _mapper);
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        var genres =  query.Handle();
        foreach (var genre in genres)
        {
            command.GenreId = genre.Id;
            FluentActions.Invoking(() => command.Handle()).Invoke();
        }

        genres = query.Handle();

        genres.Count.Should().Be(0);
        
    }

    //Happy Path
    [Fact]
    public void WhenThereAreGenresAndNoErros_Genres_ShouldBeReturned()
    {
        GetGenresQuery query = new GetGenresQuery(_context, _mapper);
        var genres =  query.Handle();
        genres.Should().NotBeNullOrEmpty();
        
    }
}