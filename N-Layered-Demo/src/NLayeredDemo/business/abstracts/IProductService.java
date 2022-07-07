package NLayeredDemo.business.abstracts;

import NLayeredDemo.entities.concretes.Product;

import java.util.List;

public interface IProductService {
    void Add( Product product);
    List<Product> GetProducts();
    Product GetProduct(int id);
}
