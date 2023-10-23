
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetGenreDetailQuery;



public class GetGenreDetailsQueryTest:IClassFixture<CommonTestFeature>
{
    private readonly IBookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetGenreDetailsQueryTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }


     //Happy Path
     [Fact]
    public void WhenGivenGenreIdIsInvalid_Genre_ShouldNotBeReturned()
    {
         
        GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
        var genre = _context.Genres.SingleOrDefault(g => g.Id == 5000);
        genre.Should().BeNull();
    }

    //Happy Path
    [Fact]
    public void WhenGivenGenreIdIsValid_Genre_ShouldBeReturned()
    {
         
        GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
        var genre = _context.Genres.SingleOrDefault(g => g.Id == 1);
        genre.Should().NotBeNull();
    }

}