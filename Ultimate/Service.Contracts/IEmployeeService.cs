using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
    EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeForCreationDto employee, bool trackChanges);

    void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
    
    //INFO: How to update an employee!
    void UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges, bool empTrackChanges);

    //INFO: HOW TO USE PATCH!
    (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch (
        Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges);
    
    void SaveChangesForPatch (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
