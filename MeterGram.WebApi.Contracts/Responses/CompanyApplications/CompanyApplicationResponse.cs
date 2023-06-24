namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications;

public class CompanyApplicationResponse
{
    public Int32 Id { get; set; }
    public String Name { get; set; } = null!;
    public String Phone { get; set; } = null!;
    public String Email { get; set; } = null!;

    public CompanyApplicationResponseCourse Course { get; set; } = default!;
    public IList<CompanyApplicationResponseParticipant> Participants { get; set; } = default!;
}