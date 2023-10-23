package Abstract;

import Concrete.Customer;

public interface ICustomerService { //if multiple objects of your structre needs the same operations, you should create an interface for the common operations and
    void Save( Customer customer ) throws Exception;
}
