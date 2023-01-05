using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace UltimateWebAPIWorkSpace.Mapper.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        //INFO: Resource=>Destination.
        // Because
        //we have the FullAddress property in our DTO record, which contains
        //both the Address and the Country from the model class, we have to
        //specify additional mapping rules with the ForMember method
        //c: destination, opt:resource
        CreateMap<Company, CompanyDto>()
            .ForMember(c=>c.FullAddress, 
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

        CreateMap<Employee, EmployeeDto>();
        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<EmployeForCreationDto, Employee>();
    }
}