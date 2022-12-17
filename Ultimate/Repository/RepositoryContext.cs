using Microsoft.EntityFrameworkCore;

namespace Repository;

//INFO: Context class is a middleware component that makes it possible to communicate with DB.
public class RepositoryContext:DbContext
{
    public RepositoryContext(DbContextOptions options)
    : base(options)
    {
        
    }

    //INFO: For database seeding!
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }

    public DbSet<Company>? Companies {get; set;} //Migration Assemblies!
    public DbSet<Employee>? Employees {get; set;}
    
}
