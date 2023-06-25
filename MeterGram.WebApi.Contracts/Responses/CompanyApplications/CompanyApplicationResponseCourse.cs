namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications;

/// <summary>
/// Application course object
/// </summary>
public class CompanyApplicationResponseCourse
{
    /// <summary>
    /// Id of course
    /// </summary>
    public Int32 Id { get; set; }

    /// <summary>
    /// Name of course
    /// </summary>
    public String Name { get; set; } = null!;

    /// <summary>
    /// Date on which the course will be held
    /// </summary>
    public DateTime Date { get; set; }
}