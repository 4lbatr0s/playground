using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;


using System;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects.Exceptions;


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

    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        if (company is null)
            throw new CompanyForCreationDtoIsNullException();
        var companyEntity = _mapper.Map<Company>(company);
        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();
        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }



    //INFO: Getting all entities from DB IS A BAD IDEA!
    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    { //INFO: We use try catch here not in the Controller!

        var companies = _repository.Company.GetAllCompanies(trackChanges);
        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies); //INFO: Destination => Resource, opposite of Mapping Profile!
        return companyDtos;
    }

    //INFO: HOW TO GET A COLLECTION OF ITEMS
    public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var companyEntities = _repository.Company.GetByIds(ids, trackChanges);
        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();
        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }

    //INFO: HOW TO CREATE A COLLECTION SERVICE!
    public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();
        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection); //Get from client and sent it to repo.
        foreach (var company in companyEntities) //TIP: create each company individually.
        {
            _repository.Company.CreateCompany(company);
        }
        _repository.Save();

        //Get its and dtos.
        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

        return (companies: companyCollectionToReturn, ids: ids); //TIP: How to return dynamic object.
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId); //INFO: Our Custom exceptions works with the Global Exception Handler.
        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }

    public void DeleteCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId); //INFO: Our Custom exceptions works with the Global Exception Handler.
        _repository.Company.DeleteCompany(company);
        _repository.Save();
    }


    //INFO: HOW TO UPDATE COMPANY, while updating company how to create children resources.
    public void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges)
    {
        var companyEntity = _repository.Company.GetCompany(companyId, trackChanges);
        if (companyEntity is null)
            throw new CompanyNotFoundException(companyId); //INFO: Our Custom exceptions works with the Global Exception Handler.
        if(companyForUpdateDto is null)
            throw new CompanyForUpdateDtoIsNullException();
        _mapper.Map(companyForUpdateDto, companyEntity);
        _repository.Save();
    
    }
}
