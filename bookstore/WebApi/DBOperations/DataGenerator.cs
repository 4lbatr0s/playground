using System;

using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if(context.Books.Any()){
                    return;
                }

                context.Books.AddRange(new Book {
                    Id = 1,
                    Title = "Dune",
                    GenreId = 1,
                    PageCount = 560,
                    PublishDate = new DateTime(1965, 10, day:23)
                });


                context.SaveChanges();
            }
        }
    }
}