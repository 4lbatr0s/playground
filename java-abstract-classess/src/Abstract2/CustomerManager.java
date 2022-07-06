package Abstract2;

public class CustomerManager {

    BaseDatabaseManagement databaseManagement; //strategy pattern!
    public void getCustomers () {
        databaseManagement.getData (); //strategy pattern!
    }
}
