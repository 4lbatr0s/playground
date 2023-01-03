namespace Shared.DataTransferObjects;


// Furthermore, we are not
// using validation attributes in this record, because we are going to use this
// record only to return a response to the client. Therefore, validation
// attributes are not required.

//INFO: init: to maintain content negotiation for xml and other response formats.
//INFO: Recods instances are immutable, therefore it's a good idea to use them for DTOS.
public record CompanyDto {
    public Guid Id {get; init;}
    public string? Name {get; init;}
    public string? FullAddress {get; init;}

}