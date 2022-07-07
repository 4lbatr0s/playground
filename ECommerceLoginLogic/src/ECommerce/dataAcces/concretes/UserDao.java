package ECommerce.dataAcces.concretes;

import ECommerce.dataAcces.abstracts.IUserDao;
import ECommerce.dataAcces.dbSimulation.DatabaseContext;
import ECommerce.dataAcces.dbSimulation.abstracts.IDBContext;
import ECommerce.entities.concretes.User;

import javax.xml.crypto.Data;
import java.util.List;

public class UserDao implements IUserDao{

    private DatabaseContext _context; //cannot use IDBContext due to lack of IoC.

    public UserDao ( ) {
        this._context = new DatabaseContext<User>();
    }

    @Override
    public void Add ( User item ) {
        _context.addValueToList ( item );

    }

    @Override
    public void Remove ( User item ) {

    }

    @Override
    public void Update ( User item ) {

    }

    @Override
    public List<User> GetAll () {
        return _context.getList ();
    }

    @Override
    public User GetById ( long id ) {
        return null;
    }
}
