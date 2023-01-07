using Entities.Exceptions;

namespace Shared.DataTransferObjects.Exceptions;

public sealed class PatchObjectIsWrongException:BadRequestException
{
    public PatchObjectIsWrongException()
    : base($"patchDoc object sent from client is null.")
    {   
        
    }
}
