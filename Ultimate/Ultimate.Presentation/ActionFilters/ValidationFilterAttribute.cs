

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ultimate.Presentation.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{

    public ValidationFilterAttribute()
    {

    }

    //TIP: Our code before action executes
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    //
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];//TIP: With RouteData.Values dictionary, we're going to get controller and action name!
        var controller = context.RouteData.Values["controller"];
        var param = context.ActionArguments //TIP: Get dto!
            .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action:{action}");
            return;
        }

        //INFO: WHAT IS ModelState: a dictionary that holds info about ModelState(in this case EmployeeForCreationDto)
        //INFO: And every key of the model state is the property of our EmployeeForCreationDto.
        //INFO: HOW TO VALIDATE!
        //TODO: Do it with ModelStateHelper class.
        if (!context.ModelState.IsValid) //TIP: if the model is not valid, then return error!
        {
            // context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            context.ModelState.AddModelError("Name", "Stupid error, don't repeat it ever again!");//INFO: We can create custom Error Messages for ModelState keys.
            var errors = new Dictionary<string, string[]>();
            foreach (var state in context.ModelState)
            {
                errors[state.Key] = state.Value.Errors.Select(error => error.ErrorMessage).ToArray();
            }
            context.Result = new UnprocessableEntityObjectResult(errors);
        }
    }
}