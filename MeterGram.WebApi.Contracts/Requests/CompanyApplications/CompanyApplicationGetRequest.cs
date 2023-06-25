namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications;

/// <summary>
/// Filter object for retrieving applications for certain company
/// </summary>
public class CompanyApplicationGetRequest : PagedRequest
{
    /// <summary>
    /// Name of company
    /// </summary>
    public string? Name { get; set; } = null!;

    /// <summary>
    /// Email of company
    /// </summary>
    public string? Email { get; set; } = null!;

    /// <summary>
    /// Phone of company
    /// </summary>
    public string? Phone { get; set; } = null!;
}