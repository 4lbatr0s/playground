using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

//TIP: We will get the id from the URI not from the body.
//TIP: to inherit a record in c#, use "abstract" keyword!
public abstract record CompanyForManipulationDto
{
    [Required(ErrorMessage = "Company name is a required field")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Company address is a required field")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Address is 100 characters.")]
    public string? Address { get; init; }
    
    [Required(ErrorMessage = "Company country is a required field")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Country is 50 characters.")]
    public string? Country { get; init; }

    IEnumerable<EmployeForCreationDto>? Employees {get; init;}//Used in CompanyForCreation.
}