package interfaces;

public class CustomerManager {

    private ICustomerDal _customerDal;
    public CustomerManager (ICustomerDal customerDal) {
        _customerDal = customerDal;
    }

    public void Add(){ //iş kodlarının yazıldıgı yer, müsteri ismi dogru girilmiş mi vb.
        _customerDal.Add ();
    }
}
