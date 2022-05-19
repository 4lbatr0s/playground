using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenreDetailsQuery
{
    private readonly BookStoreDBContext _dbContext;
    public int GenreId {get; set;}
    private readonly IMapper _mapper;

    public GetGenreDetailsQuery(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GenreDetailsView Handle()
    {
        var genre = _dbContext.Genres.Where(g => g.isActive == true).SingleOrDefault(g => g.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("Genre bulunamadi!");
        
        var vm = _mapper.Map<GenreDetailsView>(genre);

        return vm;
    }
   
}

public class GenreDetailsView 
{
    public int Id {get; set;}
    public string? Name {get; set;}
}