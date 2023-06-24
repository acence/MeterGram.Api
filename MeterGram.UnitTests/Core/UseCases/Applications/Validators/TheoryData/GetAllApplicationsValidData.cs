using MeterGram.Core.UseCases.Applications.Handlers;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

public class GetAllApplicationsValidData : TheoryData<GetAllApplications.Query>
{
    public GetAllApplicationsValidData()
    {
        Add(new GetAllApplications.Query { Name = "Test Name", Email = "a@a.com", Phone = "123", PageNumber = 1, PageSize = 10 });
    }
}