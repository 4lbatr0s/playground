
namespace Shared.DataTransferObjects;

//INFO: we dont need Id, EFCore will create it. 
public record CompanyForCreationDto(string Name, string Address, string Country);