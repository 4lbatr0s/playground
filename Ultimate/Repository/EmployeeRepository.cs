using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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


        //INFO: HOW TO CALL A COLLECTION OF ITEMS.
        public async Task<IEnumerable<Employee>> GetByIdsAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x=>  x.CompanyId.Equals(companyId) && ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            //TIP: SingleOrDefault: returns only element, if not returns null, if more than 1, throws exception.
            return await FindByCondition(e=> e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();
        }


        //INFO: HOW TO PAGING!
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters emploeyeParameters, bool trackChanges)
        {
            return await FindByCondition(e=> e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e=>e.Name)
            .Skip((emploeyeParameters.PageNumber-1) * emploeyeParameters.PageSize) 
            .Take(emploeyeParameters.PageSize)
            .ToListAsync();
            /*
                INFO: How this works? 
                1.First we get all employees,
                2.Then we say skip PageNumber times PageSize
                    2.1. Let's say we send PageNumber as 2, and PageSize as 40.
                    2.2. Its going to skip the first  80 employees!
                3.Then it will bring the PageSize(40) number of employees!
            */
        }
        
    }
}