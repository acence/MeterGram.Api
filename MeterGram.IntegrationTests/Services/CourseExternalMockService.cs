using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.CourseService;

namespace MeterGram.IntegrationTests.Services
{
    public class CourseExternalMockService : ICourseExternalService
    {
        public async Task<IList<Course>> GetCoursesAsync(bool updateCourses, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<Course>
            {
                new Course { Id = 4, Name = "Rocket Science", IsActive = true, Date = DateTime.Now.AddHours(2) },
                new Course { Id = 5, Name = "Fluid studies", IsActive = false, Date = DateTime.Now.AddHours(-5) },
            });
        }
    }
}