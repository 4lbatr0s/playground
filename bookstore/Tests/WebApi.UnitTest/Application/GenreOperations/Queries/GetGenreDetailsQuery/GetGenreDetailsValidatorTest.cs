
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetGenreDetailQuery;



public class GetGenreDetailsQueryValidatorTest:IClassFixture<CommonTestFeature>
{
    private readonly IBookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetGenreDetailsQueryValidatorTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void WhenGivenIdIsSmallerThanOne_Validator_ShouldReturnError(int genreId)
    {
        //ARRANGE
        GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
        GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
        query.GenreId = genreId;
        
        //ACT
        var results = validator.Validate(query);
 
        //ASSERT
        results.Errors.Count().Should().BeGreaterThan(0);


    }

    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void WhenGivenIdIsValid_Validator_ShouldReturnNothing(int genreId)
    {
        //ARRANGE
        GetGenreDetailsQuery query = new GetGenreDetailsQuery(_context, _mapper);
        GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
        query.GenreId = genreId;
        
        //ACT
        var results = validator.Validate(query);
 
        //ASSERT
        results.Errors.Count().Should().Be(0);


    }


}