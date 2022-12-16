using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Employee name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Age is a required field.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Position is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
    public string? Position { get; set; }

    [ForeignKey(nameof(Company))] //INFO: HOW TO IMPLEMENT CODE FIRST FOREIGN KEY!
    public Guid CompanyId { get; set; }

    public Company? Company { get; set; } //INFO: NAVIGATIONAL PROPERTY: EF CORE WILL NOT MAP THIS TO DB!



}