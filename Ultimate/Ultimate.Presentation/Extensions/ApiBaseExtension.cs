
using Entities.Responses;

namespace Ultimate.Presentation.Controllers.Extensions;

public static class ApiBaseResponseExtensions
{

    public static TResultType GetResult<TResultType>(this ApiBaseResponse
    apiBaseResponse) => ((ApiOkResponse<TResultType>)apiBaseResponse).Result;

}