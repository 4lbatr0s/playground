using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Ultimate.Presentation.Controllers
{
    //INFO: when we do companyId here, our Actions' companyId binds with this.
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


        [HttpGet("{id:guid}", Name = nameof(GetEmployeeForCompany))]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _serviceManager.EmployeeService.GetEmployee(companyId, id,
            trackChanges: false);
            return Ok(employee);
        }


        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeForCreationDto employeForCreationDto)
        {
            var employeeToReturn = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employeForCreationDto, trackChanges: false);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges:false);
            return NoContent();
        }

    }


}