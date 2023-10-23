namespace Entities.Exceptions;


//INFO: These exceptions will be used in the Service layer for COLLECTIONS
public abstract class BadRequestException : Exception
{
    //TIP: protected: its accessible from this class and derived classes.
    protected BadRequestException(string message) : base(message)
    { }
}
