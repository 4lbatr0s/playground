﻿using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection //INFO: How to create a collection!
    (IEnumerable<CompanyForCreationDto> companyCollection); 
    CompanyDto GetCompany(Guid companyId, bool trackChanges);
    CompanyDto CreateCompany(CompanyForCreationDto company);
    
    void DeleteCompany (Guid companyId, bool trackChanges);
}
