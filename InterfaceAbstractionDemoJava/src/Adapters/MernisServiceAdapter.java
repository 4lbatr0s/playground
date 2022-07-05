package Adapters;

import Abstract.ICustomerCheckService;
import Concrete.Customer;
import mernisReference.SGIKPSPublicSoap;

public class MernisServiceAdapter implements ICustomerCheckService {
    @Override
    public boolean CheckIfCustomerIsValid ( Customer customer ) throws Exception {
        mernisReference.SGIKPSPublicSoap client  = new SGIKPSPublicSoap ();
        return client.TCKimlikNoDogrula ( Long.parseLong (  customer.NatinalityId), customer.FirstName.toUpperCase (),
                customer.LastName.toUpperCase (), customer.DateOfBirth.getYear ());
    }
}