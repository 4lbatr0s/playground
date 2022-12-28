using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service;
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
}
