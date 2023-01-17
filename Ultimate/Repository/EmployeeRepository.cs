using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{

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
            return await FindByCondition(x => x.CompanyId.Equals(companyId) && ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            //TIP: SingleOrDefault: returns only element, if not returns null, if more than 1, throws exception.
            return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();
        }


        //INFO: HOW TO PAGING!
        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters emploeyeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e =>
            e.CompanyId.Equals(companyId), trackChanges)
            .Pagination(emploeyeParameters.PageNumber, emploeyeParameters.PageSize)
            .FilterEmployees(emploeyeParameters.MinAge, emploeyeParameters.MaxAge) //INFO: //INFO: FILTERING!
            .Search(emploeyeParameters.SearchTerm) //INFO: SEARCHING
            .OrderBy(e => e.Name) //INFO: pagination is in the PagedList from now...
            .ToListAsync();

            var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();


            return new PagedList<Employee>(employees, count,
             emploeyeParameters.PageNumber, emploeyeParameters.PageSize);

            /*
                INFO: How this works? 
                1.First we get all employees,
                2.Then we say skip PageNumber times PageSize
                    2.1. Let's say we send PageNumber as 2, and PageSize as 40.
                    2.2. Its going to skip the first  80 employees!
                3.Then it will bring the PageSize(40) number of employees!
                TIP: We can use PagedList.ToPagedList() instead of new PagedList
                    if we do that, we should remove Take,Skip and count.
                , but new PagedList way is much faster!
            */
        }

    }
}