package Concrete;

import Abstract.ICustomerCheckService;

public class CustomerCheckManager implements ICustomerCheckService {
    @Override
    public boolean CheckIfCustomerIsValid ( Customer customer ) {

        return true; //for now.
    }
}
