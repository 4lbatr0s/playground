public class ProductManager {

    public void Add(Product product) {
        if (ProductValidator.isValid ( product )) {
            System.out.println ("Product eklendi");
        } else {
            System.out.println ("Product uygun degil");
        }
    }
}
