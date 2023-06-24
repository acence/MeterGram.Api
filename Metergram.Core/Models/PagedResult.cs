namespace MeterGram.Core.Models;

public class PagedResult<T>
{
    public IList<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}