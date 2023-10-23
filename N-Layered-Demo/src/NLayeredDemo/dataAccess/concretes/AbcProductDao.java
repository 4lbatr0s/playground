package NLayeredDemo.dataAccess.concretes;

import NLayeredDemo.dataAccess.abstracts.IProductDao;
import NLayeredDemo.entities.concretes.Product;

import java.util.List;

public class AbcProductDao implements IProductDao {
    @Override
    public void Add ( Product product ) {
        System.out.println( "Added with Abc");
    }

    @Override
    public void Update ( Product product ) {

    }

    @Override
    public void Delete ( Product product ) {

    }

    @Override
    public Product GetById ( Product product ) {
        return null;
    }

    @Override
    public List<Product> GetAll () {
        return null;
    }
}
