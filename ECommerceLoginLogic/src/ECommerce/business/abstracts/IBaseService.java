package ECommerce.business.abstracts;

import java.util.List;

public interface IBaseService<TEntity> {
     TEntity Create(TEntity entity);
     TEntity Update(TEntity entity);
     TEntity Delete(TEntity entity);
     List<TEntity> Get();
     TEntity GetById(int id);
}
