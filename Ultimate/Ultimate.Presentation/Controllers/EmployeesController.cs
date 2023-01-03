using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Ultimate.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")] //INFO: How to PARENT/CHILD relationships in WEB API
    [ApiController]
    public class EmployeesContontroller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesContontroller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _serviceManager.EmployeeService.GetEmployees(companyId, trackChanges:
            false);
            return Ok(employees);
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _serviceManager.EmployeeService.GetEmployee(companyId, id,
            trackChanges: false);
            return Ok(employee);
        }

        

    }


}