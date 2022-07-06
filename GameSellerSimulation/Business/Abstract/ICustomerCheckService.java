package Abstract;

import Concrete.Customer;

public interface ICustomerCheckService {
    boolean CheckIfCustomerIsValid( Customer customer);
}
