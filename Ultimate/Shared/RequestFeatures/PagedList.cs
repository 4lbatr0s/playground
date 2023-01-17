namespace Shared.RequestFeatures;


//INFO:PagedList is already a list,
//therefore returning a  PagedList<T> 
//means we're returning a value of list!
public class PagedList<T>:List<T>
{
    public MetaData MetaData { get; set; } //INFO: Will be used in the Response Header.


    //INFO: pageNumber: page we want to go, pageSize: size of the page we want to go
    //INFO: items: data we have (rows)
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        this.MetaData = new MetaData{
            TotalCount  = count, //TIP:number of items
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize) //Number of pages to return, (think as a dropdown)
        };
        AddRange(items);
    }

    /// <summary>Returns the specific PagedList with MetaData and items</summary>
    public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source //INFO: PAGINATION! 
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}