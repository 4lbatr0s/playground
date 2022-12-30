using AutoMapper;
using Contracts;

namespace Service;
using Service.Contracts;


/*
    Here, as we did with the RepositoryManager class, we are utilizing the
    Lazy class to ensure the lazy initialization of our services.

    TIP:Again, we should introduce our ServiceManager class to our ServiceExtension.cs file.
*/
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _companyService = new Lazy<ICompanyService>(() => new CompanyService(loggerManager, repositoryManager, mapper));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(loggerManager, repositoryManager, mapper));
    }

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
}