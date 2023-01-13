using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository{

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            //TIP: SingleOrDefault: returns only element, if not returns null, if more than 1, throws exception.
            return await FindByCondition(e=> e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        {
            return await FindByCondition(e=> e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e=>e.Name).ToListAsync();
        }
        
    }
}