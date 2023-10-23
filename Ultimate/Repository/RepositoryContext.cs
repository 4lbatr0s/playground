using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Repository;

//INFO: Context class is a middleware component that makes it possible to communicate with DB.
public class RepositoryContext:IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options)
    : base(options)
    {
        
    }

    //INFO: For database seeding!
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder); //Configures the schema for the identity framework!
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Company>? Companies {get; set;} //Migration Assemblies!
    public DbSet<Employee>? Employees {get; set;}
    
}
