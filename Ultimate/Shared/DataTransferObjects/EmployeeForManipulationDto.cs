using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

//TIP: We will get the id from the URI not from the body.
//TIP: to inherit a record in c#, use "abstract" keyword!
public abstract record EmployeeForManipulationDto
{
    [Required(ErrorMessage = "Employee name is a required field")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name { get; init; }
    
    //INFO: If we do not use this, we'll not get any validation error for Age property because its default value is 0
    [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Position is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
    public string? Position { get; init; }
}
