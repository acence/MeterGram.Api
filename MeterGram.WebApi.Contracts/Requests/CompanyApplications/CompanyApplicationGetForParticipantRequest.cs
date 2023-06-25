namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications;

/// <summary>
/// Filter object for retrieving applications for certain participant
/// </summary>
public class CompanyApplicationGetForParticipantRequest : PagedRequest
{
    /// <summary>
    /// Name of participant
    /// </summary>
    public string? Name { get; set; } = null!;

    /// <summary>
    /// Email of participant
    /// </summary>
    public string? Email { get; set; } = null!;

    /// <summary>
    /// Phone of participant
    /// </summary>
    public string? Phone { get; set; } = null!;
}