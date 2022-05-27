using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations 
{
    public class BookStoreDBContext:DbContext, IBookStoreDBContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options):base(options)
        {
            
        }


        public DbSet<Book> Books {get; set;}
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Author> Authors {get; set;}

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Add(entity);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Remove(entity);
        }

        //IEntity'den getir.


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


    }

}