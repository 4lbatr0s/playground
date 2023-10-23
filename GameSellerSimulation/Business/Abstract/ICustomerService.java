package Abstract;

import Concrete.Customer;
import Concrete.Game;

public interface ICustomerService extends IBaseService<Customer>{
    boolean CheckIfCustomerWalletBiggerThan(Customer customer, double amount);
}

