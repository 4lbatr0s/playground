using Entities.Exceptions;

namespace Shared.DataTransferObjects.Exceptions;

public sealed class CompanyForUpdateDtoIsNullException:BadRequestException
{
    public CompanyForUpdateDtoIsNullException()
    : base($"CompanyForUpdateDto object is null")
    {   
        
    }
}

public sealed class MaxAgeRangeBadRequestException:BadRequestException
{
    public MaxAgeRangeBadRequestException()
    : base($"Max age cannot be less than min age.")
    {   
        
    }
}