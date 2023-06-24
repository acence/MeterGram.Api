namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications;

public class CompanyApplicationCreateRequest
{
    public String Name { get; set; } = null!;
    public String Phone { get; set; } = null!;
    public String Email { get; set; } = null!;

    public Int32 CourseId { get; set; }

    public IList<CompanyApplicationCreateRequestParticipant> Participants { get; set; } = default!;
}