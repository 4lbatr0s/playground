namespace Service;
using Service.Contracts;

internal sealed class EmployeeService:IEmployeeService
{
    private readonly IRepositoryManager _repository;//INFO: RepositoryManager.cs 
    private readonly ILoggerManager _logger;
    public EmployeeService(ILoggerManager logger, IRepositoryManager repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
}
