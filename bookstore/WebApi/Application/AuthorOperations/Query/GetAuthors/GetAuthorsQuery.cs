using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries;

public class GetAuthorsQuery
{
    private readonly BookStoreDBContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookStoreDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authors = _dbContext.Authors.OrderBy(a => a.Id);
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authors);//from genres, create List of AuthorsViewModel
        
        return vm;
    }
   
}

//Class icindeki Listeler automapper ile direkt olarak maplenebilir, ekstra bir işe gerek yoktur.
public class AuthorsViewModel 
{
    public string? FullName {get; set;}
    public DateTime BirthDate {get;set;}
}