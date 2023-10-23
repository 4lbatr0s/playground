

using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ultimate.Presentation.ModelBinders;

public class ArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {

        //TIP: 1. first we need to check whether our parameter is Enumerable ? 
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        //2.TIP: we extract the value (a comma seperated string of GUIDs)
        var providedValue = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName)
            .ToString();
        //3.TIP: if GUIDs are empty we return success with empty, in CompanyService GetByIds we check whether its empty or not.
        if (string.IsNullOrEmpty(providedValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        //4.TIP: generic type will store the type of IEnumerable list which is GUID in our case.
        var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
        //5.TIP: we created a converter for GUID, this will convert string to Guid, if we wanted to convert to int it would because its generic.
        var converter = TypeDescriptor.GetConverter(genericType);
        //6.TIP: we create an array of GUIDs.
        var objectArray = providedValue.Split(new[] { "," },
    StringSplitOptions.RemoveEmptyEntries)
    .Select(x => converter.ConvertFromString(x.Trim()))
    .ToArray();
        //7.TIP: we create a copy of objectArray.
        var guidArray = Array.CreateInstance(genericType, objectArray.Length);
        objectArray.CopyTo(guidArray, 0);

        //8.TIP: we sent our guidArray to bindingContext as its model.
        bindingContext.Model = guidArray;
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}