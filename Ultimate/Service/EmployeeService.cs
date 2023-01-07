using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Exceptions;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;//INFO: RepositoryManager.cs 
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public EmployeeService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeForCreationDto employee, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        if (employee is null)
            throw new EmployeeForCreationDtoIsNullException();
        var employeeEntity = _mapper.Map<Employee>(employee);
        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();
        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;

    }

    public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);
        _repository.Employee.DeleteEmployee(employeeEntity);
        _repository.Save();
    }

    public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);
        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employee;
    }

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        var employees = _repository.Employee.GetEmployees(companyId, trackChanges);
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeeDtos;
    }


    //INFO: HOW TO PATCH REQUEST!
    public (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, compTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId,
        empTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(companyId);
        
            
        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }
    //INFO: HOW TO PATCH REQUEST!
    public void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        _repository.Save();
    }

    //INFO: How to update an employee!
    //INFO: If we update an object, we should follow their trackChanges.
    public void UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges, bool empTrackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, compTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, empTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);
        if (employeeForUpdateDto is null)
            throw new EmployeeForUpdateDtoIsNullException();
        _mapper.Map(employeeForUpdateDto, employeeEntity); //TIP: HOW TO UPDATE WITH MAPPING.
        _repository.Save();
    }
}