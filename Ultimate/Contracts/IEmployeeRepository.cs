
using Entities.Models;
using Shared.RequestFeatures;
namespace Contracts
{
    //INFO: We will use this repositories to create custom functions for our services besides the RepositoryBase class. 
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters emploeyeParameters,  bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
         Task<IEnumerable<Employee>> GetByIdsAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges); //INFO: HOW TO GET COLLECTION OF ITEMS!

    }
}

