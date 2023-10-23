package Concrete;

import Abstract.BaseCustomerManager;
import Abstract.ICustomerCheckService;
import Abstract.ICustomerService;

public class StarbucksCustomerManager extends BaseCustomerManager { //we implicitely implement ICustomerService interface too because BAseCustomerManager implements it.


    ICustomerCheckService _customerCheckService;

    public StarbucksCustomerManager ( ICustomerCheckService _customerCheckService ) {
        this._customerCheckService = _customerCheckService;
    }

    @Override
    public void Save ( Customer customer ) throws Exception {
        if(_customerCheckService.CheckIfCustomerIsValid ( customer )) //validation, MERNIS.
            super.Save ( customer ); //We took the super.Save method that does the saving job on Database, but we need to do a MERNIS authentication before it.
        else
            throw new Exception ("Not a valid customer");
    }
}

