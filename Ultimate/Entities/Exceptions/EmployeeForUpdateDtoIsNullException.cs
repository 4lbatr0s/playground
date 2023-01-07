using Entities.Exceptions;

namespace Shared.DataTransferObjects.Exceptions;

public sealed class EmployeeForUpdateDtoIsNullException:BadRequestException
{
    public EmployeeForUpdateDtoIsNullException()
    : base($"EmployeeForUpdateDto object is null")
    {   
        
    }
}
