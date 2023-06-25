using MeterGram.Domain.Models;

namespace MeterGram.IntegrationTests.SeedData
{
    public static class CourseSeedData
    {
        public static IList<Course> Get()
        {
            return new List<Course>
            {
                new Course { Id = 1, Name = "Intro to Astrophysics", IsActive = true, Date = DateTime.Now },
                new Course { Id = 2, Name = "Advanced trigonometry", IsActive = true, Date = DateTime.Now.AddDays(1) },
                new Course { Id = 3, Name = "Calculus 1", IsActive = false, Date = DateTime.Now.AddDays(-1) },
                new Course { Id = 4, Name = "Rocket Science", IsActive = false, Date = DateTime.Now.AddHours(2) },
                new Course { Id = 5, Name = "Fluid studies", IsActive = true, Date = DateTime.Now.AddHours(-5) },
            };
        }
    }
}