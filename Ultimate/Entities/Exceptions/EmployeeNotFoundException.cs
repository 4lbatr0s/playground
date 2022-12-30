namespace Entities.Exceptions;


//INFO: sealed: not extendable.
public sealed class EmployeeNotFoundException:NotFoundException
{
    public EmployeeNotFoundException(Guid employeeId)
    : base($"The employee with id: {employeeId} doesn't exist in the database")
    {

    }
}