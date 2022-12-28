using System.Runtime.InteropServices.ObjectiveC;
using System.Text.Json;


namespace Models.ErrorModel;



//INFO:We are going to use this class for the details of our error message.
public class ErrorDetail
{
    public int StatusCode { get; set; }
    public string? Message { get; set;}

    public override string ToString() => JsonSerializer.Serialize(this);
    
}