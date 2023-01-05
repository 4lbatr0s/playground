namespace Shared.DataTransferObjects.Exceptions;

public sealed class EmployeeForCreationDtoIsNullException:BaseException
{
    public EmployeeForCreationDtoIsNullException()
    : base($"Employee object is null")
    {   
        
    }
}