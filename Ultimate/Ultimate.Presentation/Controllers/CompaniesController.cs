using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Ultimate.Presentation.ModelBinders;

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
        public async Task<IActionResult> GetCompanies()
        {
            //testing the global exception:
            // throw new Exception("Exception");
            var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]//INFO: Our path is api/companies/id, our id's type is GUID!
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);
            return Ok(company);
        }


        //INFO: We can also use FromUri, but its not reccomended.        
        [HttpPost] //INFO: FromBody: we are not going to take values from URI, we will get them from body, 2. the object is complex, therefore we shoudl use FromBody.
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var createdCompany = await _serviceManager.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany); //INFO: How to return a static object.
        }


        //INFO: How to return a collection of items!
        //INFO: we have created a ModelBinding and used it on GetCompanyCollection, because we are obligated to send our ids as String.
        [HttpGet("collection/{ids}", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = await _serviceManager.CompanyService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(companies);
        }


        //INFO: HOW TO CREATE A COLLECTION!
        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);

            /*
                So why we return strings to CompanyCollection(GetCompanyCollection) when it requires IEnumerable as ids?
                Because CreatedAtRoute cannot create Location header with List, but it can create it with String.
            */
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{companyId:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _serviceManager.CompanyService.DeleteCompanyAsync(companyId, trackChanges: false);
            return NoContent();
        }

        //INFO: How to update a parent resource.
        [HttpPut("{companyId:guid}")]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] CompanyForUpdateDto companyForUpdateDto)
        {
            await _serviceManager.CompanyService.UpdateCompanyAsync(companyId, companyForUpdateDto, trackChanges: true);
            return NoContent();
        }


    }
}