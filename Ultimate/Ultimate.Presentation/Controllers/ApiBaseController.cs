
using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ErrorModel;

namespace Ultimate.Presentation.Controllers;


/*
INFO: Before we start changing the actions in the CompaniesController , we
have to create a way to handle error responses and return them to the
client – similar to what we have with the global error handler middleware.
We are not going to create any additional middleware but another
controller base class inside the Presentation/Controllers folder:
*/
public class ApiControllerBase : ControllerBase
{
    public IActionResult ProcessError(ApiBaseResponse baseResponse)
    {
        return baseResponse switch
        {
            ApiNotFoundResponse => NotFound(new ErrorDetail
            {
                Message = ((ApiNotFoundResponse)baseResponse).Message,
                StatusCode = StatusCodes.Status404NotFound
            }),
            ApiBadRequestResponse => BadRequest(new ErrorDetail
            {
                Message = ((ApiBadRequestResponse)baseResponse).Message,
                StatusCode = StatusCodes.Status400BadRequest
            }),
            _ => throw new NotImplementedException()
        };
    }
}