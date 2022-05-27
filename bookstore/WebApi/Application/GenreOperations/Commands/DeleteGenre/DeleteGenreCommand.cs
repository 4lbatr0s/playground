

using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands;

public class DeleteGenreCommand
{
    private readonly IBookStoreDBContext _dbContext;
    public int GenreId { get; set; }
    public DeleteGenreCommand(IBookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(){
        var genre = _dbContext.Genres.Where(g => g.isActive == true).SingleOrDefault(g => g.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("Genre mevcut degil!");
        _dbContext.Remove(genre);
        _dbContext.SaveChanges();
    }


}
