namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications;

/// <summary>
/// Request to apply for a course
/// </summary>
public class CompanyApplicationCreateRequest
{
    /// <summary>
    /// Name of company
    /// </summary>
    public String Name { get; set; } = null!;

    /// <summary>
    /// Phone of company
    /// </summary>
    public String Phone { get; set; } = null!;

    /// <summary>
    /// Email of company
    /// </summary>
    public String Email { get; set; } = null!;

    /// <summary>
    /// Course to apply to
    /// </summary>
    public Int32 CourseId { get; set; }

    /// <summary>
    /// List of participants
    /// </summary>
    public IList<CompanyApplicationCreateRequestParticipant> Participants { get; set; } = default!;
}