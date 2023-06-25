using MeterGram.WebApi.Contracts.Requests.CompanyApplications;

namespace MeterGram.IntegrationTests.TestData
{
    public class CreateNewApplicationTestData : TheoryData<CompanyApplicationCreateRequest>
    {
        public CreateNewApplicationTestData()
        {
            Add(new CompanyApplicationCreateRequest()
            {
                Name = "Seavus",
                Email = "a@a.com",
                Phone = "123",
                CourseId = 1,
                Participants = new List<CompanyApplicationCreateRequestParticipant> { 
                    new CompanyApplicationCreateRequestParticipant() { 
                        Name = "Aleksandar Trajkov",
                        Email = "a@a.com",
                        Phone = "123",
                    }
                }
            });
        }
    }
}