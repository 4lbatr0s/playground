

using System.Linq.Expressions;

public interface IRepositoryBase<T>
{
    //INFO: This will be inherited from RepositoryBase class. 
    //TODO: I'M ON PAGE 42 IN THE BOOK
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    
    
}