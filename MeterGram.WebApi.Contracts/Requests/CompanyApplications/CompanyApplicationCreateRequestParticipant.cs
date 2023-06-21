namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications
{
    public class CompanyApplicationCreateRequestParticipant
    {
        public String Name { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String Email { get; set; } = null!;
    }
}