
using Entities.LinkModels;
using Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;
namespace Contracts;


public interface IEmployeeLinks
{
    LinkResponse TryGenerateLinks (IEnumerable<EmployeeDto> employeeDtos, string fields, Guid companyId, HttpContext httpContext);
}