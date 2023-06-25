namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications;

/// <summary>
/// Single participant to the course application
/// </summary>
public class CompanyApplicationResponseParticipant
{
    /// <summary>
    /// Participant Id
    /// </summary>
    public Int32 Id { get; set; }

    /// <summary>
    /// Participant name
    /// </summary>
    public String Name { get; set; } = null!;

    /// <summary>
    /// Participant phone
    /// </summary>
    public String Phone { get; set; } = null!;

    /// <summary>
    /// Participant email
    /// </summary>
    public String Email { get; set; } = null!;
}