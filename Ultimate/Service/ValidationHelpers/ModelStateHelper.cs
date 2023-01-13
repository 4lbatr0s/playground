
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http;
namespace Service.ValidationHelpers;


public class ModelStateHelper : IActionResult
{
   private readonly object _value; //TIP: first we create an object type, we will return this.

    public ModelStateHelper(object value) //TIP: then we create a constructor to return this value.
    {
        _value = value;
    }

    ///<summary>
    ///This code defines a method that is part of an IActionResult implementation.
    /// The ExecuteResultAsync method is called by the MVC framework to execute the action result and generate an HTTP response
    // to be sent to the client.
    ///</summary>
    public Task ExecuteResultAsync(ActionContext context)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
        };

        return Task.FromResult(response);
    }

    //INFO: HOW TO CREATE A METHOD THAT CAN BE RETURNED AS ACTION RESULT
    public static IActionResult ReturnUnprocessableEntityWithErrors(ModelStateDictionary modelState)
    {
        var errors = new Dictionary<string, string[]>();
        foreach (var state in modelState)
        {
            errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
        }
        return new ModelStateHelper(errors);
    }
}