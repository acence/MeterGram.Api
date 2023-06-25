namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications;

/// <summary>
/// Participant for course application
/// </summary>
public class CompanyApplicationCreateRequestParticipant
{
    /// <summary>
    /// Name of participant
    /// </summary>
    public String Name { get; set; } = null!;

    /// <summary>
    /// Phone of participant
    /// </summary>
    public String Phone { get; set; } = null!;

    /// <summary>
    /// Email of partipant
    /// </summary>
    public String Email { get; set; } = null!;
}