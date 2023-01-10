using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
    Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeForCreationDto employee, bool trackChanges);

    Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges);
    
    //INFO: How to update an employee!
    Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges, bool empTrackChanges);

    //INFO: HOW TO USE PATCH!
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync (
        Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges);
    
    Task SaveChangesForPatchAsync (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
