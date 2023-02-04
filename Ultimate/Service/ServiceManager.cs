using AutoMapper;
using Contracts;

namespace Service;

using Entities.Models;
using Entities.Models.ConfigurationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.DataTransferObjects;


/*
    Here, as we did with the RepositoryManager class, we are utilizing the
    Lazy class to ensure the lazy initialization of our services.

    TIP:Again, we should introduce our ServiceManager class to our ServiceExtension.cs file.
*/
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    


    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IEmployeeLinks employeeLinks, 
    UserManager<User> userManager, IOptionsMonitor<JwtConfiguration> configuration)
    {
        _companyService = new Lazy<ICompanyService>(() => new CompanyService(loggerManager, repositoryManager, mapper));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(loggerManager, repositoryManager, mapper, employeeLinks));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, mapper, userManager, configuration));
    }

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}