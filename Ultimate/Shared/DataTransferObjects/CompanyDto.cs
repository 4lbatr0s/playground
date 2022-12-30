namespace Shared.DataTransferObjects;


// Furthermore, we are not
// using validation attributes in this record, because we are going to use this
// record only to return a response to the client. Therefore, validation
// attributes are not required.
public record CompanyDto(Guid Id, string Name, string FullAddress);//INFO: Recods instances are immutable, therefore it's a good idea to use them for DTOS.
