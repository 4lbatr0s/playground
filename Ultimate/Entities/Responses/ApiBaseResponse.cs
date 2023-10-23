

using System.Drawing;

namespace Entities.Responses;

/*
This is an abstract class, which will be the main return type for all of our
methods where we have to return a successful result or an error result. It
also contains a single Success property stating whether the action was
successful or not.
*/
public abstract class ApiBaseResponse
{
    public bool Success { get; set; }

    public ApiBaseResponse(bool success) => Success = success; //Set Success to success.
}
