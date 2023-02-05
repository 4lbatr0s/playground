using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;


using System;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects.Exceptions;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Entities.Responses;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;//INFO: RepositoryManager.cs 
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CompanyService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company)
    {
        if (company is null)
            throw new CompanyForCreationDtoIsNullException();
        var companyEntity = _mapper.Map<Company>(company);
        _repository.Company.CreateCompany(companyEntity);
        await _repository.Save();
        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }



    //INFO: Getting all entities from DB IS A BAD IDEA!
    public async Task<ApiBaseResponse> GetAllCompaniesAsync(bool trackChanges)
    { //INFO: We use try catch here not in the Controller!

        var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);
        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies); //INFO: Destination => Resource, opposite of Mapping Profile!
        return new ApiOkResponse<IEnumerable<CompanyDto>>(companyDtos);
    }

    //INFO: HOW TO GET A COLLECTION OF ITEMS
    public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChanges);
        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();
        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }

    //INFO: HOW TO CREATE A COLLECTION SERVICE!
    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();
        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection); //Get from client and sent it to repo.
        foreach (var company in companyEntities) //TIP: create each company individually.
        {
            _repository.Company.CreateCompany(company);
        }
        await _repository.Save();

        //TIP:Get Ids and DTOs of every company.
        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

        return (companies: companyCollectionToReturn, ids: ids); //TIP: How to return dynamic object.
    }

    public async Task<ApiBaseResponse> GetCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
        if (company == null)
            return new CompanyNotFoundResponse(companyId);
        var companyDto = _mapper.Map<CompanyDto>(company);
        return new ApiOkResponse<CompanyDto>(companyDto);
    }

    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
        _repository.Company.DeleteCompany(company);
        await _repository.Save();
    }


    //INFO: HOW TO UPDATE COMPANY, while updating company how to create children resources.
    public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges)
    {
        var companyEntity = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
        if(companyForUpdateDto is null)
            throw new CompanyForUpdateDtoIsNullException();
        _mapper.Map(companyForUpdateDto, companyEntity);
        await _repository.Save();
    
    }

    //INFO: Private methods for implementing the DRY principle
    private async Task<Company> GetCompanyAndCheckIfItExists(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        return company;
    } 
}
