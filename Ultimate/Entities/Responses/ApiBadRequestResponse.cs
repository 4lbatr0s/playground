namespace Entities.Responses;


public abstract class ApiBadRequestResponse : ApiBaseResponse
{
    //TIP: ApiBadRequestResponse will have a message and because of ApiBaseResponse it will have a Success Property too!
    public string Message { get; set; }

    public ApiBadRequestResponse(string message)
    : base(false)
    {
        Message = message;
    }
}
