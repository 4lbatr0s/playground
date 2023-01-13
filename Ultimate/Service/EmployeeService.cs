using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Exceptions;
using Shared.RequestFeatures;

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

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeForCreationDto employee, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);
        if (employee is null)
            throw new EmployeeForCreationDtoIsNullException();
        var employeeEntity = _mapper.Map<Employee>(employee);
        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        await _repository.Save();
        var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;

    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);
        var employeeEntity = await GetEmploeyeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);
        _repository.Employee.DeleteEmployee(employeeEntity);
        await _repository.Save();
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);
        var employeeEntity = await GetEmploeyeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);
        var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);
        return employeeDto;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters,  bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);
        var employees = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges); //TIP: PAGING!
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeeDtos;
    }


    //INFO: HOW TO PATCH REQUEST!
    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid employeeId, bool compTrackChanges, bool empTrackChanges)
    {
        
        await CheckIfCompanyExist(companyId, compTrackChanges);
        var employeeEntity = await GetEmploeyeeForCompanyAndCheckIfItExists(companyId, employeeId, empTrackChanges);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }
    //INFO: HOW TO PATCH REQUEST!
    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repository.Save();
    }

    //INFO: How to update an employee!
    //INFO: If we update an object, we should follow their trackChanges.
    public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges, bool empTrackChanges)
    {
        await CheckIfCompanyExist(companyId, compTrackChanges);
        var employeeEntity = await GetEmploeyeeForCompanyAndCheckIfItExists(companyId, employeeId, empTrackChanges);

        if (employeeForUpdateDto is null)
            throw new EmployeeForUpdateDtoIsNullException();
        _mapper.Map(employeeForUpdateDto, employeeEntity); //TIP: HOW TO UPDATE WITH MAPPING.
        await _repository.Save();
    }


    public async Task<(IEnumerable<EmployeeDto> employees, string employeeIds)> CreateEmployeeForCompanyCollectionAsync(Guid companyId, IEnumerable<EmployeForCreationDto> employeeCollection, bool compTrackChanges, bool empTrackChanges)
    {
        if (employeeCollection is null)
            throw new EmployeeCollectionBadRequest();
        await CheckIfCompanyExist(companyId, compTrackChanges);
        var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);
        foreach (var employee in employeeEntities)
        {   
            _repository.Employee.CreateEmployeeForCompany(companyId, employee);
        }
        await _repository.Save();

        //TIP: GET IDS AND DTOS OF EVERY COMPANY.
        var employeeCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        var ids = string.Join(", ", employeeCollectionToReturn.Select(c => c.Id));

        return (employees: employeeCollectionToReturn, employeeIds:ids);
    }

        //INFO: HOW TO GET A COLLECTION OF ITEMS
    public async Task<IEnumerable<EmployeeDto>> GetByIdsAsync(Guid companyId, IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        await CheckIfCompanyExist(companyId, trackChanges);
        var employeeEntities = await _repository.Employee.GetByIdsAsync(companyId, ids, trackChanges);
        if (ids.Count() != employeeEntities.Count())
            throw new CollectionByIdsBadRequestException();
        var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        return employeesToReturn;
    }
  


    //INFO: PRIVATE FUNCTIONS TO IMPLEMENT DRY APPROACH!
    private async Task CheckIfCompanyExist(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company is null)
            throw new CompanyNotFoundException(companyId);
    }
    private async Task<Employee> GetEmploeyeeForCompanyAndCheckIfItExists(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);
        if(employee is null)
            throw new EmployeeNotFoundException(employeeId);
        return employee;
    }

}