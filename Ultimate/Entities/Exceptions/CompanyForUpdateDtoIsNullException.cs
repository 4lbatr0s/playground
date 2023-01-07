using Entities.Exceptions;

namespace Shared.DataTransferObjects.Exceptions;

public sealed class CompanyForUpdateDtoIsNullException:BadRequestException
{
    public CompanyForUpdateDtoIsNullException()
    : base($"CompanyForUpdateDto object is null")
    {   
        
    }
}