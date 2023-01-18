
using Shared.RequestFeatures;

namespace Shared.RequestFeatures;


public class EmployeeParameters:RequestParameters
{

    public EmployeeParameters ()=> OrderBy = "name"; //INFO: default value of the sorting.

    //INFO: uint to prevent negative values!
    public uint MinAge { get; set; } //TIP: default value is 0.
    public uint MaxAge { get; set; } = int.MaxValue;//TIP:Default value is MAxValue
    public bool ValidAgeRange => MaxAge > MinAge;
    public string? SearchTerm { get; set; }
}