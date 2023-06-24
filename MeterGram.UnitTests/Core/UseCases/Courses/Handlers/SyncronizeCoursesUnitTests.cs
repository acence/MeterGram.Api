using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.CourseService;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.UnitTests.Core.UseCases.Courses.Handlers;

public class SyncronizeCoursesUnitTests
{
    private readonly Mock<ICourseRepository> _courseRepository;
    private readonly Mock<ICourseExternalService> _courseExternalService;

    public SyncronizeCoursesUnitTests()
    {
        _courseRepository = new Mock<ICourseRepository>();

        _courseExternalService = new Mock<ICourseExternalService>();
        _courseExternalService.Setup(x => x.GetCoursesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Course> { new Course() });
    }

    [Fact]
    public async Task WhenCalling_SyncroniseCourses_ExpectSaveMethodToBeCalled()
    {
        var handler = new SynchronizeCourses(_courseExternalService.Object, _courseRepository.Object);
        var command = new SynchronizeCourses.Command();
        await handler.Handle(command, CancellationToken.None);

        _courseExternalService.Verify(x => x.GetCoursesAsync(It.IsAny<Boolean>(), It.IsAny<CancellationToken>()), Times.Once);
        _courseRepository.Verify(x => x.BulkUpsertWithIdentity(It.IsAny<IList<Course>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task WhenCalling_SyncroniseCoursesWithoutRepository_ExpectException()
    {
        Func<Task> result = async () => await new SynchronizeCourses(_courseExternalService.Object, null!).Handle(new SynchronizeCourses.Command(), CancellationToken.None);

        var exception = await Record.ExceptionAsync(result);
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact]
    public async Task WhenCalling_SyncroniseCoursesWithoutExternalService_ExpectException()
    {
        Func<Task> result = async () => await new SynchronizeCourses(null!, _courseRepository.Object).Handle(new SynchronizeCourses.Command(), CancellationToken.None);

        var exception = await Record.ExceptionAsync(result);
        exception.Should().BeOfType<ArgumentNullException>();
    }
}