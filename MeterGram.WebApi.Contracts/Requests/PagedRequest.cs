namespace MeterGram.WebApi.Contracts.Requests;

/// <summary>
/// Class meant for inheritance to other requests
/// </summary>
public abstract class PagedRequest
{
    /// <summary>
    /// Current page number to be retrieved, starts from 1
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page size to be retrieved
    /// </summary>
    public int PageSize { get; set; } = 100;
}