namespace Entities.Exceptions;


//INFO: These exceptions will be used in the Service layer.
public abstract class NotFoundException:Exception
{
    //TIP: protected: its accessible from this class and derived classes.
    protected NotFoundException(string message):base(message)
    {}
}