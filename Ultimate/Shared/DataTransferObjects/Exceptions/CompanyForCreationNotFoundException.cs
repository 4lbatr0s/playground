namespace Shared.DataTransferObjects.Exceptions;


//INFO: sealed: not extendable.
public sealed class CompanyForCreationDtoIsNullException:BaseException
{
    public CompanyForCreationDtoIsNullException()
    : base($"CompanyForCreationDto object is null")
    {   
        
    }
}