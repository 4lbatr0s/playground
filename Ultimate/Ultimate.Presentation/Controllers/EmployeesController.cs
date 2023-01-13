using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.ValidationHelpers;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Ultimate.Presentation.ActionFilters;
using Ultimate.Presentation.ModelBinders;

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


        //INFO: HOW TO IMPLEMENT PAGING!
        //INFO: We say that we will get employeeParameters from query. If we send a query like helloWorld=10,
        //That means we can ge this query like employeeParameters.helloWorld
        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = await _serviceManager.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges:
            false);
            return Ok(employees);
        }


        [HttpGet("{id:guid}", Name = nameof(GetEmployeeForCompany))]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeAsync(companyId, id,
            trackChanges: false);
            return Ok(employee);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeForCreationDto employeForCreationDto)
        {

            //INFO: WE ARE DOING OUR VALIDATION WITH OUR VALIDATIONFILTERATTRIBUTE ACTION FILTER!

            //INFO: WHAT IS ModelState: a dictionary that holds info about ModelState(in this case EmployeeForCreationDto)
            //INFO: And every key of the model state is the property of our EmployeeForCreationDto.
            //INFO: HOW TO VALIDATE!
            //TODO: Do it with ModelStateHelper class.
            // if (!ModelState.IsValid)
            // {
            //     ModelState.AddModelError("Name", "Stupid error, don't repeat it ever again!");//INFO: We can create custom Error Messages for ModelState keys.
            //     var errors = new Dictionary<string, string[]>();
            //     foreach (var state in ModelState)
            //     {
            //         errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
            //     }
            //     return UnprocessableEntity(errors);
            // }

            var employeeToReturn = await _serviceManager.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employeForCreationDto, trackChanges: false);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            await _serviceManager.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
        {
            //INFO: When we pass empTrackChanges:true, EF Core follows the changes in the employee object with the employeeId 
            //and converts its status to Modified
            // if (!ModelState.IsValid)
            // {
            //     ModelState.AddModelError("Name", "Stupid error, don't repeat it ever again!");//INFO: We can create custom Error Messages for ModelState keys.
            //     var errors = new Dictionary<string, string[]>();
            //     foreach (var state in ModelState)
            //     {
            //         errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
            //     }
            //     return UnprocessableEntity(errors);
            // }
            await _serviceManager.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, employeeId, employeeForUpdateDto, compTrackChanges: false, empTrackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");
            var result = await _serviceManager.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
            compTrackChanges: false,
            empTrackChanges: true);

            //INFO: HOW TO VALIDATE PATCH REQUEST!, WE NEED TO USE NEWTONSOFT JSON.
            patchDoc.ApplyTo(result.employeeToPatch, ModelState);
            TryValidateModel(result.employeeToPatch); //INFO: IF we do not use this, validation errors with Remove patch will not be noticed!
            // if(!ModelState.IsValid)
            //     return UnprocessableEntity(ModelState);

            await _serviceManager.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch,
            result.employeeEntity);
            return NoContent();
        }



        //INFO: How to return a collection of items!
        //INFO: we have created a ModelBinding and used it on GetCompanyCollection, because we are obligated to send our ids as String.
        //TIP: employeeIds: should be same as in the parameters: employeeIds
        [HttpGet("collection/{employeeIds}", Name = "EmployeeCollection")]
        public async Task<IActionResult> GetEmployeeCollection(Guid companyId, [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> employeeIds)
        {
            var employees = await _serviceManager.EmployeeService.GetByIdsAsync(companyId, employeeIds, trackChanges: false);
            return Ok(employees);
        }


        //INFO: HOW TO CREATE A COLLECTION!
        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompanyCollection(Guid companyId, [FromBody] IEnumerable<EmployeForCreationDto> employeeCollection)
        {
            var result = await _serviceManager.EmployeeService.CreateEmployeeForCompanyCollectionAsync(companyId, employeeCollection, compTrackChanges:false, empTrackChanges:false);

            /*
                So why we return strings to CompanyCollection(GetCompanyCollection) when it requires IEnumerable as ids?
                Because CreatedAtRoute cannot create Location header with List, but it can create it with String.
            */
            return CreatedAtRoute("EmployeeCollection", new {companyId, result.employeeIds }, result.employees);
            // return Ok(result.employees);
        }

    }


}