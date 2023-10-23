using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges); //INFO: HOW TO GET COLLECTION OF ITEMS!
        
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        

        void CreateCompany(Company company); //INFO: give company as parameter.

        void DeleteCompany(Company company);//INFO: Its cascade, it will delete the children objects.
    }
}

