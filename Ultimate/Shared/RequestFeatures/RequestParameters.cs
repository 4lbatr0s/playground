

namespace Shared.RequestFeatures;


/*
INFO:
We create an abstract class to hold the common properties for all the
entities in our project.
*/
public abstract class RequestParameters
{
    const int maxPageSize = 50; //INFO: To restrict api 50 rows per page!
    public int PageNumber { get; set; } = 1; //INFO: How to create default value for property with get set!
    private int _pageSize = 10; //Number of rows
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }
    public string? OrderBy { get; set; }
}