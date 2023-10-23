using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenresQuery
{
    private readonly IBookStoreDBContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = _dbContext.Genres.Where(g => g.isActive == true).OrderBy(g => g.Id);
        List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genres);//from genres, create List of GenresViewModel
        return vm;
    }
   
}

public class GenresViewModel 
{
    public int Id {get; set;}
    public string? Name {get; set;}
}