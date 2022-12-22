using Contracts;
using Entities.Models;

namespace Repository{

    //TODO: I'M AT PAGE 45 IN THE BOOK.
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
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
    }
}