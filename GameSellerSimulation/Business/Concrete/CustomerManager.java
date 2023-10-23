package Concrete;

import Abstract.ICustomerCheckService;
import Abstract.ICustomerService;

import java.util.List;

public class CustomerManager  implements ICustomerService {

    private ICustomerCheckService _checkService;

    public CustomerManager ( ICustomerCheckService _customerCheckService ) {
        this._checkService = _customerCheckService;
    }


    @Override
    public Customer Create ( Customer entity ) {
        if(_checkService.CheckIfCustomerIsValid ( entity)){
            System.out.println("Data is valid, customer created");
            return entity;
        } else{
            System.out.println("Data is not valid");
            return null;
        }
    }

    @Override
    public Customer Remove ( Customer entity ) {
        return entity;
    }

    @Override
    public Customer Update ( Customer entity ) {
        return entity;
    }

    @Override
    public List<Customer> GetAll () {
        return null;
    }

    @Override
    public Customer GetById ( String id ) {
        return null;
    }

    public boolean CheckIfCustomerWalletBiggerThan(Customer customer, double amount ) {
        return customer.getWalletAmount ()>=amount ? true : false;
    }
}
