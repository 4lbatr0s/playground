namespace Shared.DataTransferObjects.Exceptions;


//INFO: These exceptions will be used in the Service layer.
public abstract class BaseException:Exception
{
    //TIP: protected: its accessible from this class and derived classes.
    protected BaseException(string message):base(message)
    {}
}