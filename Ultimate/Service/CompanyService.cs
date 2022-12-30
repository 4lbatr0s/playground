using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service;

using System;
using Entities.Exceptions;
using Service.Contracts;

internal sealed class CompanyService:ICompanyService
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

    //INFO: Getting all entities from DB IS A BAD IDEA!
    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    { //INFO: We use try catch here not in the Controller!

            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies); //INFO: Destination => Resource, opposite of Mapping Profile!
            return companyDtos;
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if(company is null)
            throw new CompanyNotFoundException(companyId); //INFO: Our Custom exceptions works with the Global Exception Handler.
        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
}
