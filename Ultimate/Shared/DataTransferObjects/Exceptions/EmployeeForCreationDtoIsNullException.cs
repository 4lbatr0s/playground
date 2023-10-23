namespace Shared.DataTransferObjects.Exceptions;

public sealed class EmployeeForCreationDtoIsNullException:BaseException
{
    public EmployeeForCreationDtoIsNullException()
    : base($"EmployeeForCreationDtoIsNull object is null")
    {   
        
    }
}
