

using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands;

public class CreateGenreCommand
{
    private readonly IMapper _mapper;
    private readonly IBookStoreDBContext _dbContext;
    public CreateGenreModel Model;
    public CreateGenreCommand(IMapper mapper, IBookStoreDBContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public void Handle(){
        var genre = _dbContext.Genres.Where(g => g.isActive == true).SingleOrDefault(g => g.Name == Model.Name);
        if(genre is not null)
            throw new InvalidOperationException("Genre zaten mevcut!");

        genre = _mapper.Map<Genre>(Model);
        _dbContext.Add(genre);
        _dbContext.SaveChanges();
    }


}

public class CreateGenreModel{
    public string? Name { get; set; }
}