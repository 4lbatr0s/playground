

using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands;

public class UpdateGenreCommand
{
    private readonly BookStoreDBContext _dbContext;

    public UpdateGenreCommand(BookStoreDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GenreUpdateModel Model;
    public int GenreId { get; set; }

    public void Handle(){
        var genre = _dbContext.Genres.SingleOrDefault(g => g.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("Genre mevcut degil!");
        genre.Name = Model.Name != default ? Model.Name : genre.Name;
        genre.isActive = Model.isActive != default ? Model.isActive : genre.isActive;
    
        _dbContext.SaveChanges();
    }
}

public class GenreUpdateModel
{
    public string? Name {get; set;}
    public bool isActive {get; set;}
}