
using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions.Utilities;


public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString)
    {
        var orderParams = orderByQueryString.Trim().Split(','); //"", "age desc"
        //TIP: we need properties of T in order to check whether "age or name" fields exist in the T.
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrEmpty(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];//in order to split "age desc" and get the age.
            //Bring the first property or return null, compare property's name and orderParamter not considering the Case Sensitivity.
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
            //if we dont find property in the T, we continue with the next orderParameter.
            if (objectProperty != null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending"; //get the direction for every orderParameter.
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}"); //Example: "Name ascending, DateOfBirth descending"
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); //remove the trim and the spaces at the end

        return orderQuery;
    }
}