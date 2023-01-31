using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ultimate.Presentation.Controllers
{
    [Route("api")] //INFO: How to PARENT/CHILD relationships in WEB API
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator; //TIP: We're going to create links for the endpoints.

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.ultimate.apiroot"))
            {
                //INFO: We only create links for the ROOT requests, Root request: /api/companies 
                //INFO: We're not creating any list for the employees because EmployeesController is children of the CompaniesController!
                var list = new List<Link>
                {
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {}),
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, "GetCompanies", new {}),
                        Rel = "companies",
                        Method = "GET"
                    },
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, "CreateCompany", new {}),
                        Rel = "create_company",
                        Method = "POST"
                    },
                };
                return Ok(list);
            }
            return NoContent();
        }
    }
}