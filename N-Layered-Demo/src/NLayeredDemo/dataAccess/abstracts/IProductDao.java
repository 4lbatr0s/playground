package NLayeredDemo.dataAccess.abstracts;

import NLayeredDemo.entities.concretes.Product;

import java.util.List;

//ProductRepository is used too same meaning!
public interface IProductDao {
    void Add( Product product);
    void Update( Product product);
    void Delete( Product product);
    Product GetById( Product product);
    List<Product> GetAll(); //Instead of List, we can use ArrayList too. ArrayList implements  List.
}
