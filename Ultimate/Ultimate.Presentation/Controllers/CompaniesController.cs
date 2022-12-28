using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Ultimate.Presentation.Controllers
{
    [Route("api/companies")] //TIP: Best practice, always use name in the route!
    [ApiController]
    public class CompaniesController:ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet] //TIP: route of this action will be api/companies.
        public IActionResult GetCompanies()
        {
                //testing the global exception:
                throw new Exception("Exception");
                var companies=  _serviceManager.Company.GetAllCompanies(trackChanges:false);
                return Ok(companies);
        }
    }
}