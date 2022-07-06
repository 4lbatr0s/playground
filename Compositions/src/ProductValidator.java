public class ProductValidator {

    static{ //static, ProductValidator calistirildiğinda calisan bir yapidir.
        System.out.println("Static yapici block calisti!");
    }
    static{
        System.out.println("Static yapici block 2 calisti!"); //birden cok static kullanilabilir.
    }
    public static boolean isValid (Product product) {
        if (product.price >10 && product.name.isEmpty ()) {
            return true;
        } else {
            return false;
        }
    }
}
