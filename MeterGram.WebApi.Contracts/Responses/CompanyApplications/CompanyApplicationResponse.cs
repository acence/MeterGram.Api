namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications;

/// <summary>
/// Company application response object
/// </summary>
public class CompanyApplicationResponse
{
    /// <summary>
    /// Application Id
    /// </summary>
    public Int32 Id { get; set; }

    /// <summary>
    /// Company name
    /// </summary>
    public String Name { get; set; } = null!;

    /// <summary>
    /// Company phone
    /// </summary>
    public String Phone { get; set; } = null!;

    /// <summary>
    /// Company email
    /// </summary>
    public String Email { get; set; } = null!;

    /// <summary>
    /// Course for which the application was made
    /// </summary>
    public CompanyApplicationResponseCourse Course { get; set; } = default!;

    /// <summary>
    /// List of participants
    /// </summary>
    public IList<CompanyApplicationResponseParticipant> Participants { get; set; } = default!;
}