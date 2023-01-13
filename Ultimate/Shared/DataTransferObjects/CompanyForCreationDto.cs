
namespace Shared.DataTransferObjects;

//INFO: we dont need Id, EFCore will create it. 
//INFO: we dont need to change service or repository because Employees is nullable  and the dont have a representation in the db.

// public record CompanyForCreationDto(string Name, string Address, string Country,
    // IEnumerable<EmployeForCreationDto> Employees);

public record CompanyForCreationDto:CompanyForManipulationDto;