package ECommerce.dataAcces.abstracts;

import java.util.List;

public interface IBaseDao <TEntity>{

     void Add(TEntity item);
     void Remove(TEntity item);
     void Update(TEntity item);
     List<TEntity> GetAll();
     TEntity GetById(long id);



}
