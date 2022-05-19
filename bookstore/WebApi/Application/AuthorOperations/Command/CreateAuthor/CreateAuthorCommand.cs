
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations;

public class CreateAuthorCommand
{
    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper _mapper;
    public CreateAuthorModel? Model;
    public CreateAuthorCommand(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(a => a.Name == Model.Name || a.Surname == Model.Surname);
        if(author is not null)
            throw new InvalidOperationException("Böyle bir yazar zaten var!");
    
        author = _mapper.Map<Author>(Model);
        _dbContext.Add(author);
        _dbContext.SaveChanges();
    }


}

public class CreateAuthorModel
{
    public string? Name { get; set; }
    public string? Surname {get; set;}
    public DateTime BirthDay {get; set;}
    public int BookId {get; set;}
}