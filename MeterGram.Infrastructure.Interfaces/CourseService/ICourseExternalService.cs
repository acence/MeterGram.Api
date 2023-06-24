using MeterGram.Domain.Models;

namespace MeterGram.Infrastructure.Interfaces.CourseService;

public interface ICourseExternalService
{
    Task<IList<Course>> GetCoursesAsync(Boolean updateCourses, CancellationToken cancellationToken);
}
