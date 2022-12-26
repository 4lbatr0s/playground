using AutoMapper;
using Contracts;

namespace Service;
using Service.Contracts;

internal sealed class EmployeeService:IEmployeeService
{
    private readonly  IRepositoryManager _repository;//INFO: RepositoryManager.cs 
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public EmployeeService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }
    
}
