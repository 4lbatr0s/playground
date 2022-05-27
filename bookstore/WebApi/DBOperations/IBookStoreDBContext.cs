using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations;


public interface IBookStoreDBContext
{
    DbSet<Book> Books {get;set;}
    DbSet<Genre> Genres {get; set;}
    DbSet<Author> Authors {get; set;}


    int SaveChanges();
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

}