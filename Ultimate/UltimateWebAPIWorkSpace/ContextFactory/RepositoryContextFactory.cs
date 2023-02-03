
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;
namespace UltimateWebAPIWorkSpace.ContextFactory;

/*
    INFO: WHY TO USE THIS FACTORY ? 

    The RepositoryContextFactory is used for design-time support for Entity Framework Core. 
    IDesignTimeDbContextFactory interface provides a way to create instances of a DbContext type that are specifically
    intended for design-time services such as migrations and scaffolding.
    The RepositoryContextFactory class implements the IDesignTimeDbContextFactory interface and provides 
    a CreateDbContext method for creating instances of the RepositoryContext.
    The purpose of having a separate factory class is to provide a way to create the context
    without having to set up the entire application, which is typically required to create 
    an instance of the context using the standard constructor. 
    This makes it easier to use the context during design-time tasks 
    such as migrations and scaffolding, as it provides a way to create 
    a context instance without having to set up the entire application.
*/
    


//INFO: EntityFrameworkCore.Desing is installed to use IDesignTimeDbContextFactory
public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{

    //INFO: How to create a RepositoryContextFactory
    public RepositoryContext CreateDbContext(string[] args)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())//INFO: going to give us currentrepository.
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"),
                b => b.MigrationsAssembly("UltimateWebAPIWorkSpace") //TIP:We have to to this, because migration assemblies are not in the main project, they are in the Repository.
                ); //INFO: GetConnection String is an extension for GetSection function!

        return new RepositoryContext(builder.Options);
    }
}