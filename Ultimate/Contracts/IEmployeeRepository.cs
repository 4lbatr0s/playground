

using Entities.Models;

namespace Contracts
{
    //INFO: We will use this repositories to create custom functions for our services besides the RepositoryBase class. 
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
    }
}

