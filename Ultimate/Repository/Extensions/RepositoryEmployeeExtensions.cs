

using Entities.Models;

namespace Repository.Extensions;

//TODO: CREATE A GENERIC REPOSITORY PATTERN FOR THESE EXTENSIONS
public static class RepositoryEmployeeExtensions
{
    /// <summary>
    /// Filters a collection of employees by a specified age range.
    /// </summary>
    /// <param name="employees">The collection of employees to filter</param>
    /// <param name="minAge">The minimum age of employees to include in the filtered collection</param>
    /// <param name="maxAge">The maximum age of employees to include in the filtered collection</param>
    /// <returns>The filtered collection of employees that match the specified age range</returns>
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
    {
        return employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));
    }

    /// <summary>
    /// Search for employees by name and returns the matching employees
    /// </summary>
    /// <param name="employees">The collection of employees to search</param>
    /// <param name="searchTerm">The term to search for within the employee names</param>
    /// <returns>The filtered collection of employees that match the search term</returns>
    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return employees;
        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Employee> Pagination(this IQueryable<Employee> employees, int pageNumber, int pageSize)
    {
        return employees.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}