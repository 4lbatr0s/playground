
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;//TIP: To understand how this works, checkout CompanyRepository or EmployeeRepository.

    public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;


    public IQueryable<T> FindAll(bool trackChanges) =>
    !trackChanges ?
    RepositoryContext.Set<T>()
    .AsNoTracking() : //INFO: We use AsNoTracking to improve our readonly query performance, we declare EF Core does not need to track changes for the required entities
    RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges
        ? RepositoryContext.Set<T>()
            .Where(expression)
                .AsNoTracking()
        : RepositoryContext.Set<T>()
            .Where(expression);

    }

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}