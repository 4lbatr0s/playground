namespace Entities.Responses;

public sealed class ApiOkResponse<TResult> : ApiBaseResponse
{
    public TResult Result { get; set; } //TIP: This will be our CONCRETE result (resource)
    public ApiOkResponse(TResult result) : base(true) //TIP: Set Success property of the ApiBaseResponse class to TRUE!
    {
        Result = result;
    }
}
