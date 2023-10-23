using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi;
using WebApi.Application.BookOperations.CreateBook.Commands;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;

namespace Application.BookOperations.Queries;


public class GetBookDetailsQueryValidatorTest:IClassFixture<CommonTestFeature>
{
    private readonly IBookStoreDBContext _context;
    private readonly IMapper _mapper;
    public GetBookDetailsQueryValidatorTest(CommonTestFeature commonTestFeature)
    {
        _context = commonTestFeature.Context;
        _mapper = commonTestFeature.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void WhenInvalidBookIdIsGiven_Validator_ShouldThrowError(int bookId)
    {

        //act
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        //bir command olusturmak zorundayız cünkü commandı validate ediyoruz.
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query); //errorlari dönelim.


        //assert
        result.Errors.Count.Should().BeGreaterThan(0);


        
    }

}