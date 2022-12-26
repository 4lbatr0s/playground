using Contracts;
using Entities.Models;
namespace Repository{

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

        //INFO: We implements RepositoryBases' FindAll funtion under a different function name, then we will call this from service.
        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}