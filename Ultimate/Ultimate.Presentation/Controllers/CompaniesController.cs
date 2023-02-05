using Entities.Responses;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Ultimate.Presentation.ActionFilters;
using Ultimate.Presentation.Controllers.Extensions;
using Ultimate.Presentation.ModelBinders;

namespace Ultimate.Presentation.Controllers
{
    [ApiVersion("1.0")] //TIP: DEFAULT API VERSION, IF CLIENT DOES NOT SPECIFY THE DETAIL, THIS WILL BE THE API VERSION
    [Route("api/companies")] //TIP: Best practice, always use name in the route!
    // [ResponseCache(CacheProfileName ="120SecondsDuration")]        
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CompaniesController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        /// <summary>
        /// Gets the list of all companies
        /// </summary>
        /// <returns>The companies list</returns>
        //TIP: route of this action will be api/companies.
        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> GetCompanies()
        {
            //testing the global exception:
            // throw new Exception("Exception");
            var baseResult = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            var companies = baseResult.GetResult<IEnumerable<CompanyDto>>();
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]//INFO: Our path is api/companies/id, our id's type is GUID!
        // [ResponseCache(Duration = 60)] //INFO: Maven.Cache.Headers  library will configure this!
        [HttpCacheExpiration(CacheLocation=CacheLocation.Public, MaxAge=60)]//Marvin.Cache.Headers resource level configs example
        [HttpCacheValidation(MustRevalidate =false)]//Marvin.Cache.Headers resource level configs example

        public async Task<IActionResult> GetCompany(Guid id)
        {
            var baseResult = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);
            if(!baseResult.Success)
                return ProcessError(baseResult);
            var company = baseResult.GetResult<CompanyDto>();
            return Ok(company);
        }

        /// <summary>
        /// Creates a newly created company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>A newly created company</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="422">If the model is invalid</response>
        //INFO: We can also use FromUri, but its not reccomended.        
        [HttpPost] //INFO: FromBody: we are not going to take values from URI, we will get them from body, 2. the object is complex, therefore we shoudl use FromBody.
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] CompanyForUpdateDto companyForUpdateDto)
        {
            await _serviceManager.CompanyService.UpdateCompanyAsync(companyId, companyForUpdateDto, trackChanges: true);
            return NoContent();
        }

        //INFO: How to create OPTIONS  request.
        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Alow", "GET,OPTIONS,POST");
            return Ok();
        }


    }
}