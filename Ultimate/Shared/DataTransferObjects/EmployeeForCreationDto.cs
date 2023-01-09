
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

//INFO: We need to use validation with put,post,patch requests.
//INFO: If we want to use validation we better use init keyword
public record EmployeForCreationDto:EmployeeForManipulationDto;