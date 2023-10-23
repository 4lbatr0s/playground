

using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Contracts;
using Entities.Models;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class
{
    public PropertyInfo[] Properties { get; set; }

    public DataShaper()
    {
        //INFO: How to get properties of a class

        // BindingFlags is an enumeration that provides a set of values to filter the properties 
        //that are returned by the Type.GetProperties() method.

        // Public specifies that only public properties should be returned,
        //and Instance specifies that only instance properties should be returned, not static properties.
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance); //Get properties.
        //INFO: HOW TO GET PROPERTIES OF A CLASS!
    }
    public IEnumerable<Entity> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchData(entities, requiredProperties);
    }

    public Entity ShapeData(T entity, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties).Entity;
    }

    //INFO: Private methods!
    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
    {
        var requiredProperties = new List<PropertyInfo>();
        if (!string.IsNullOrWhiteSpace(fieldsString))
        {
            var fields = fieldsString.Split(',', //TIP: fields=name, age  == ["name", "age]
            StringSplitOptions.RemoveEmptyEntries); //remove spaces.
            foreach (var field in fields)//["name", "age"]
            {
                var property = Properties //bring the name-Name-NAME-naME... property
                .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                StringComparison.InvariantCultureIgnoreCase));
                if (property == null) //check if given property name in the url exists in the T Entity.
                    continue;
                requiredProperties.Add(property); //add property to list.  [{name:"Name", type:"string"}...]
            }
        }
        else
        {
            requiredProperties = Properties.ToList();//if fields query is empty, bring all properties!
        }

        return requiredProperties;
    }


    private IEnumerable<Entity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedData = new List<Entity>();
        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedData.Add(shapedObject.Entity);
        }
        return shapedData; //TIP: return list of shapedObjects! [{name:value, name:value}, ....{name:value, name:value..etc}]
    }

    //INFO: Brings property values of the entity's required properties..
    private ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {

        //INFO: ExpandObject implements IDictionary<string, object> interface
        var shapedObject = new ShapedEntity();
        foreach (var property in requiredProperties)
        {
            //here we get the required values from the T entity!
            var objectPropertyValue = property.GetValue(entity); //TIP: property:Name, value:Mike
            shapedObject.Entity.TryAdd(property.Name, objectPropertyValue); //TIP: shapedObject is an ExpandoOBJECT therefore a Dictionary!:
        }
        var objectProperty = entity.GetType().GetProperty("Id"); 
        shapedObject.Id = (Guid)objectProperty.GetValue(entity);

        return shapedObject; 
    }
}