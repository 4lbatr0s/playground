using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.ValidationHelpers;
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
            //INFO: WHAT IS ModelState: a dictionary that holds info about ModelState(in this case EmployeeForCreationDto)
            //INFO: And every key of the model state is the property of our EmployeeForCreationDto.
            //INFO: HOW TO VALIDATE!
            //TODO: Do it with ModelStateHelper class.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Stupid error, don't repeat it ever again!");//INFO: We can create custom Error Messages for ModelState keys.
                var errors = new Dictionary<string, string[]>();
                foreach (var state in ModelState)
                {
                    errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
                }
                return UnprocessableEntity(errors);
            }

            var employeeToReturn = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employeForCreationDto, trackChanges: false);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
        {
            //INFO: When we pass empTrackChanges:true, EF Core follows the changes in the employee object with the employeeId 
            //and converts its status to Modified
            //TODO: Do it with ModelStateHelper class.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Stupid error, don't repeat it ever again!");//INFO: We can create custom Error Messages for ModelState keys.
                var errors = new Dictionary<string, string[]>();
                foreach (var state in ModelState)
                {
                    errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
                }
                return UnprocessableEntity(errors);
            }
            _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, employeeId, employeeForUpdateDto, compTrackChanges: false, empTrackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");
            var result = _serviceManager.EmployeeService.GetEmployeeForPatch(companyId, id,
            compTrackChanges: false,
            empTrackChanges: true);
 
            //INFO: HOW TO VALIDATE PATCH REQUEST!, WE NEED TO USE NEWTONSOFT JSON.
            patchDoc.ApplyTo(result.employeeToPatch, ModelState);
            TryValidateModel(result.employeeToPatch); //INFO: IF we do not use this, validation errors with Remove patch will not be noticed!
            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.EmployeeService.SaveChangesForPatch(result.employeeToPatch,
            result.employeeEntity);
            return NoContent();
        }


    }


}