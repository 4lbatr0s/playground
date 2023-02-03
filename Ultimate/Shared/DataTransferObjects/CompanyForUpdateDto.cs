namespace Shared.DataTransferObjects;


//INFO: We can create children resources while updating a parent resource, look at the Employees!
// public record CompanyForUpdateDto (string Name, string Address, string Country, IEnumerable<EmployeForCreationDto> Employees);
public record CompanyForUpdateDto : CompanyForManipulationDto;
