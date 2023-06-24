namespace MeterGram.WebApi.Contracts.Responses
{
    /// <summary>
    /// Wrapper for paged results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        /// <summary>
        /// The data set in the paged result
        /// </summary>
        public IList<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Current requested page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Requested page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total items based on current request criteria
        /// </summary>
        public int TotalItems { get; set; }
    }
}