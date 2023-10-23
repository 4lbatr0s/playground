using WebApi;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDBContext context)
    {
         context.Authors.AddRange(new Author {
                    Name = "Ernst",
                    Surname = "Hemingway"
                });
    }
}