using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{

    internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        /*
            INFO:  How to implement RepositoryContext: 
                We will create a repository instance here, let's say its companyRepository.
                Then we will assign it to repositoryContext.
                : base(repositoryContext) means go to RepositoryBase class, execute the Constructor that takes single parameter with type RepositoryContext

        */
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        
        /*
        INFO: Create and Delete operations that are coming from EF Core DBContext do not change anything 
            in the db actually. They just change the state of the instances that we send.
            Therefore they're not I/O operations.They're executed on the Internal Cache of the DBContext.
            Which makes them fast enough.
        */
        public void CreateCompany(Company company)
        {
            Create(company);//INFO: comes from RepositoryBase.
        }

        public void DeleteCompany(Company company) => Delete(company);

        //INFO: We implements RepositoryBases' FindAll funtion under a different function name, then we will call this from service.
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        //INFO: HOW TO CALL A COLLECTION OF ITEMS.
        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x=> ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefaultAsync();
    }
}