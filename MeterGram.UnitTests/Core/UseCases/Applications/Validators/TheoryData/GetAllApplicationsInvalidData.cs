using MeterGram.Core.Behaviours;
using MeterGram.Core.UseCases.Applications.Handlers;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

public class GetAllApplicationsInvalidData : TheoryData<GetAllApplications.Query, IList<(string property, string errorCode)>>
{
    public GetAllApplicationsInvalidData()
    {
        Add(new GetAllApplications.Query { Name = new string('A', 500), Email = "a@a.com", Phone = "123", PageNumber = 1, PageSize = 10 }, new List<(string, string)> { ("Name", ValidationErrorCodes.MaximumLength) });
        Add(new GetAllApplications.Query { Name = "TestName", Email = "a@a.com", Phone = new string('1', 100), PageNumber = 1, PageSize = 10 }, new List<(string, string)> { ("Phone", ValidationErrorCodes.MaximumLength)});
        Add(new GetAllApplications.Query { Name = "TestName", Email = "a@a.com", Phone = "asd", PageNumber = 1, PageSize = 10 }, new List<(string, string)> { ("Phone", ValidationErrorCodes.NotValidContent) });
        Add(new GetAllApplications.Query { Name = "TestName", Email = new string('A', 200), Phone = "123", PageNumber = 1, PageSize = 10 }, new List<(string, string)> { ("Email", ValidationErrorCodes.MaximumLength), ("Email", ValidationErrorCodes.Email) });
        Add(new GetAllApplications.Query { Name = "TestName", Email = "a", Phone = "123", PageNumber = 1, PageSize = 10 }, new List<(string, string)> { ("Email", ValidationErrorCodes.Email) });
        Add(new GetAllApplications.Query { Name = "TestName", Email = "a@a.com", Phone = "123", PageNumber = 0, PageSize = 10 }, new List<(string, string)> { ("PageNumber", ValidationErrorCodes.GreaterThan) });
        Add(new GetAllApplications.Query { Name = "TestName", Email = "a@a.com", Phone = "123", PageNumber = 1, PageSize = -1 }, new List<(string, string)> { ("PageSize", ValidationErrorCodes.GreaterThanOrEqualTo) });
    }
}