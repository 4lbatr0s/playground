package Concrete;
import Abstract.IEntity;
import java.util.Date;

public class Customer implements IEntity {
    public int id;
    public String FirstName;
    public String LastName;
    public String NatinalityId;
    public Date DateOfBirth;
}
