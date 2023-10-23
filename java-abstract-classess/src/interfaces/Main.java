package interfaces;

public class Main {
    public static void main ( String[] args ) {
        ICustomerDal customerDal = new OracleCustomerDal ();
        ICustomerDal customerDal2 = new MySqlCustomerDal ();
        customerDal.Add ();
        customerDal2.Add ();

        CustomerManager customerManager = new CustomerManager  (new MySqlCustomerDal ());
        customerManager.Add ();
    }
}
