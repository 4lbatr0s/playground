using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        
        IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges); //INFO: HOW TO GET COLLECTION OF ITEMS!
        
        Company GetCompany(Guid companyId, bool trackChanges);
        
        void CreateCompany(Company company); //INFO: give company as parameter.

        void DeleteCompany(Company company);//INFO: Its cascade, it will delete the children objects.
    }
}

