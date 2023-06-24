using MeterGram.Infrastructure.CourseService.Models;

namespace MeterGram.Infrastructure.CourseService.Services.Interfaces;

public interface ICourseHttpService
{
    Task<CourseListWrapperResponseModel> GetCoursesAsync(Uri uri, String token, CancellationToken cancellationToken = default);
    Task<String> GetTokenAsync(CancellationToken cancellationToken = default);
}