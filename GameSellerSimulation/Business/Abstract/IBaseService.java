package Abstract;

import java.util.List;

public interface IBaseService<T>
{
    T Create(T entity);
    T Remove(T entity);
    T Update(T entity);
    List<T> GetAll();
    T GetById(String id);
}

