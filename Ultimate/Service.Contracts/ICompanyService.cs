using Entities.Models;
using Entities.Responses;
using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface ICompanyService
{
    Task<ApiBaseResponse> GetAllCompaniesAsync(bool trackChanges);
    Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync //INFO: How to create a collection!
    (IEnumerable<CompanyForCreationDto> companyCollection); 
    Task<ApiBaseResponse> GetCompanyAsync(Guid companyId, bool trackChanges);
    Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
    
    Task DeleteCompanyAsync  (Guid companyId, bool trackChanges);
    Task UpdateCompanyAsync (Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);
}
