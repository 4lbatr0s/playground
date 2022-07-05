package Abstract;

import Concrete.Customer;

/*Why to use this abstract class ?
* Because both, NeroCustomerManager and StarbucksCustomerManager classes have got an operation that is completely same.
* This operation is "Save". So, instead of implementing ICustomerService to the classess, we create an abstract class and implement ICustomerService
* on this abstract class.Then implement abstract class on the CustomerManager classes.
* */
public abstract class BaseCustomerManager implements ICustomerService{
    @Override
    public void Save ( Customer customer ) throws Exception {
    System.out.println( "saved to db!" + customer.FirstName);
    }
}
