namespace MeterGram.WebApi.Contracts.Responses
{
    public class PagedResponse<T>
    {
        public IList<T> Data { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}