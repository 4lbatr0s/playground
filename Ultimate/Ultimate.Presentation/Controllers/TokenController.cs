using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Service.Contracts;
using Shared.DataTransferObjects;
using Ultimate.Presentation.ActionFilters;

namespace Ultimate.Presentation.Controllers
{

    /*  INFO: WHY A SEPERATE CONTROLLER ?
        A separate TokenController is used for implementing refresh tokens because it provides separation of concerns and modularity in the application's architecture. 
        The TokenController is specifically responsible for handling refresh token requests, while the other controllers can focus on handling other parts of the application. 
        This helps to keep the code organized and maintainable, and can also make it easier to update and test the refresh token functionality independently.
    */
    [Route("api/token")] //INFO: How to PARENT/CHILD relationships in WEB API
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TokenController(IServiceManager service) => _service = service;


        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await
            _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}