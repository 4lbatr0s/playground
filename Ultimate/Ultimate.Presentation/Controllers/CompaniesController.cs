using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Ultimate.Presentation.Controllers
{
    [Route("api/companies")] //TIP: Best practice, always use name in the route!
    [ApiController]
    public class CompaniesController : ControllerBase
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
            // throw new Exception("Exception");
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]//INFO: Our path is api/companies/id, our id's type is GUID!
        public IActionResult GetCompany(Guid id)
        {
            var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }


        //INFO: We can also use FromUri, but its not reccomended.        
        [HttpPost] //INFO: FromBody: we are not going to take values from URI, we will get them from body, 2. the object is complex, therefore we shoudl use FromBody.
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var createdCompany = _serviceManager.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new {id=createdCompany.Id}, createdCompany); //INFO: How to return a static object.
        }
    }
}