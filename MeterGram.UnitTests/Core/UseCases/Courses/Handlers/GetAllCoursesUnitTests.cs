using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.UnitTests.Core.UseCases.Courses.Handlers;

public class GetAllCoursesUnitTests
{
    private readonly Mock<ICourseRepository> _courseRepository;
    public GetAllCoursesUnitTests()
    {
        _courseRepository = new Mock<ICourseRepository>();
        _courseRepository.Setup(x => x.GetAllCourseAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Course> { new Course() });
    }

    [Fact]
    public async Task WhenCalling_GetAllCourses_ExpectListOfCourses()
    {
        var handler = new GetAllCourses(_courseRepository.Object);
        var query = new GetAllCourses.Query();
        var response = await handler.Handle(query, CancellationToken.None);

        response.Should().NotBeNull();
        response.Count().Should().Be(1);
    }

    [Fact]
    public async Task WhenCalling_GetAllCoursesWithoutRepository_ExpectException()
    {
        Func<Task> result = async () => await new GetAllCourses(null!).Handle(new GetAllCourses.Query(), CancellationToken.None);

        var exception = await Record.ExceptionAsync(result);
        exception.Should().BeOfType<ArgumentNullException>();
    }
}