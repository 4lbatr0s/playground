package NLayeredDemo.business.concretes;

import NLayeredDemo.business.abstracts.IProductService;
import NLayeredDemo.core.logger.abstracts.ILoggerService;
import NLayeredDemo.dataAccess.abstracts.IProductDao;
import NLayeredDemo.entities.concretes.Product;

import java.util.List;

public class ProductManager  implements IProductService {
    /*We should write BUSINESS RULE CODES HERE.
    *We use Dependency Injection to create Loosely connected systems!
    *
    * */

    private IProductDao _productDao; // Loosely connected!
    private ILoggerService _loggerService; // Loosely connected
    public ProductManager ( IProductDao _productDao, ILoggerService loggerService ) {
        super();
        this._productDao = _productDao;
        _loggerService = loggerService;
    }


    @Override
    public void Add ( Product product ) {
        if(product.getCategoryId () == 1){
            System.out.println("Product category is not accepted");
            return;
        }

        _productDao.Add ( product );
        _loggerService.Log ( product.getName () ) ;
    }

    @Override
    public List<Product> GetProducts () {
        return null;
    }

    @Override
    public Product GetProduct ( int id ) {
        return null;
    }
}
