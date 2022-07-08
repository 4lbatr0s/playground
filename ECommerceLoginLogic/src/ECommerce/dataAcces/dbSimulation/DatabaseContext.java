package ECommerce.dataAcces.dbSimulation;

import ECommerce.dataAcces.dbSimulation.abstracts.IDBContext;
import ECommerce.entities.concretes.User;

import java.util.ArrayList;
import java.util.List;

public class DatabaseContext<T>  implements IDBContext {


    private List<T> list;

    public DatabaseContext (  ) {
        this.list = new ArrayList<T>();
    }

    private void createList () {
        this.list = new ArrayList<T>();
    }

    public List<T> getList () {
        return list;
    }

    public void addValueToList (T obj ) {
        this.list.add( obj );
    }

    public void removeValueFromList ( T obj ) {
        this.list.remove ( obj );
    }

}
