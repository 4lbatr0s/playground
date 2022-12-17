
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;


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