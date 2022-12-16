using Microsoft.EntityFrameworkCore;

namespace Repository;

//INFO: Context class is a middleware component that makes it possible to communicate with DB.
public class RepositoryContext:DbContext
{
    public RepositoryContext(DbContextOptions options)
    : base(options)
    {
        
    }

    public DbSet<Company>? Companies {get; set;}
    public DbSet<Employee>? Employees {get; set;}
    
}
