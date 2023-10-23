using WebApi;
using WebApi.DBOperations;

namespace TestSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDBContext context)
    {
        context.Books.AddRange(
            new Book{Id = 1, Title = "Dune", GenreId = 1, AuthorId = 1, PageCount = 560, PublishDate = new DateTime(1965, 10, 23)},
            new Book{Id = 2, Title = "Dune Messiah", GenreId = 1, AuthorId = 1, PageCount = 600, PublishDate = new DateTime(1968, 10, 25 )},
            new Book{Id = 2, Title = "Dune God Emperor", GenreId = 1, AuthorId = 1, PageCount = 800, PublishDate = new DateTime(1971, 15, 12 )});
    }
}