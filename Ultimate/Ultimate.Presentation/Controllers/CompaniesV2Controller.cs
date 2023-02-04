using System.Runtime.Serialization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Ultimate.Presentation.ActionFilters;
using Ultimate.Presentation.ModelBinders;

namespace Ultimate.Presentation.Controllers
{   
    [ApiVersion("2.0")]            
    [Route("api/{v:apiversion}/companies")] //TIP:HOW TO SEND API VERSION
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CompaniesV2Controller : ControllerBase
    {
        
        private readonly IServiceManager _service;

        public CompaniesV2Controller(IServiceManager service)
        {
            _service = service;
        }

        //INFO: Classic
        // [HttpGet]
        // public async Task <IActionResult> GetCompanies()
        // {
        //     var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges:false);
        //     return Ok(companies);
        // }

        //INFO: HOW TO USE URL VERSIONING:we get the version from path parameters!
        [HttpGet]
        public async Task <IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges:false);
            var companiesV2 = companies.Select(c=> $"{c.Name} V2");
            return Ok(companiesV2);
        }
    }
}