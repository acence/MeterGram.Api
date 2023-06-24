using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Handlers;

public class GetAllApplicationsForParticipantUnitTests
{
    private readonly Mock<ICompanyApplicationRepository> _companyApplicationRepository;
    public GetAllApplicationsForParticipantUnitTests()
    {
        _companyApplicationRepository = new Mock<ICompanyApplicationRepository>();
        _companyApplicationRepository.Setup(x => x.GetAllForParticipantAsync(
            It.IsAny<Int32>(),
            It.IsAny<Int32>(),
            It.IsAny<String?>(),
            It.IsAny<String?>(),
            It.IsAny<String?>(),
            It.IsAny<CancellationToken>())).ReturnsAsync((5, new List<CompanyApplication> { new CompanyApplication() }));

    }
    [Fact]
    public async Task WhenCalling_GetAllCourses_ExpectListOfCourses()
    {
        var handler = new GetAllApplicationsForParticipant(_companyApplicationRepository.Object);
        var query = new GetAllApplicationsForParticipant.Query();
        var response = await handler.Handle(query, CancellationToken.None);

        response.Should().NotBeNull();
        response.Data.Should().NotBeNull();
        response.Data.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenCalling_GetAllCoursesWithoutRepository_ExpectException()
    {
        Func<Task> result = async () => await new GetAllApplicationsForParticipant(null!).Handle(new GetAllApplicationsForParticipant.Query(), CancellationToken.None);

        var exception = await Record.ExceptionAsync(result);
        exception.Should().BeOfType<ArgumentNullException>();
    }
}