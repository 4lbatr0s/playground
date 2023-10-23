using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetAuthorDetailsQuery
{
    private readonly IBookStoreDBContext _dbContext;
    private readonly IMapper _mapper;

    public int AuthorId { get; set; }

    public GetAuthorDetailsQuery(IBookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AuthorDetailsView Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if(author is null)
            throw new InvalidOperationException("Yazar bulunamadi");
        
        AuthorDetailsView vm = _mapper.Map<AuthorDetailsView>(author);//from genres, create List of AuthorsViewModel
                
        return vm;
    }
   
}

//Class icindeki Listeler automapper ile direkt olarak maplenebilir, ekstra bir işe gerek yoktur.
public class AuthorDetailsView 
{
    public string? FullName {get; set;}
    
    public DateTime BirthDate{get; set;}
    public List<Book> Books {get; set;}

}