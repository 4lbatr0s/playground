namespace Shared.DataTransferObjects;

public record EmployeeForUpdateDto(string Name, int Age, string Position);//TIP: We will get the id from the URI not from the body.