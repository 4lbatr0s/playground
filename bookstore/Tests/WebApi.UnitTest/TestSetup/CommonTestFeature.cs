using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;

namespace TestSetup;

public class CommonTestFeature
{
    public BookStoreDBContext Context {get; set;} 
    public IMapper Mapper {get; set;}

    public CommonTestFeature()
    {
        //database.
        var options = new DbContextOptionsBuilder<BookStoreDBContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDBContext").Options;
        Context = new BookStoreDBContext(options);
        Context.Database.EnsureCreated();
        // Context.AddBooks();
        Context.AddGenres();
        Context.AddAuthors();
        Context.SaveChanges();

        //mapper
        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}