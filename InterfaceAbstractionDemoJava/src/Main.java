import Abstract.BaseCustomerManager;
import Adapters.MernisServiceAdapter;
import Concrete.Customer;
import Concrete.StarbucksCustomerManager;

import java.util.Date;

public class Main {
    public static void main(String[] args) throws Exception {
        BaseCustomerManager customerManager = new StarbucksCustomerManager (new MernisServiceAdapter ());
        Customer customer = new Customer();
        customer.FirstName = "John";
        customer.LastName = "Johnny";
        customer.NatinalityId  ="123041241";
        customer.id = 1;
        customer.DateOfBirth = new Date(2021,10,12);
        customerManager.Save(customer);


     }


}