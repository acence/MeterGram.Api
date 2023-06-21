namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications
{
    public class CompanyApplicationResponseParticipant
    {
        public Int32 Id { get; set; }
        public String Name { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String Email { get; set; } = null!;
    }
}