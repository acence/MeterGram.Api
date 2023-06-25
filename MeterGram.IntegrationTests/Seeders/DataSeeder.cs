using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeterGram.IntegrationTests.Seeders
{
    public static class DataSeeder
    {
        public static void Seed(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>() as DatabaseContext;

                try
                {
                    appContext!.Database.EnsureCreated();

                    foreach (var project in GetCourses())
                    {
                        appContext.Entry(project).State = EntityState.Added;
                    }
                    foreach(var application in GetCompanyApplications())
                    {
                        appContext.Entry(application).State = EntityState.Added;
                        foreach(var participant in application.Participants)
                        {
                            appContext.Entry(participant).State = EntityState.Added;
                        }
                    }
                    appContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Log errors
                    throw;
                }

            }
        }
        private static IList<Course> GetCourses()
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

        private static IList<CompanyApplication> GetCompanyApplications()
        {
            return new List<CompanyApplication>
            {
                new CompanyApplication {
                    Id = 1,
                    Name = "MeterGram",
                    Email = "a@a.com",
                    Phone = "123",
                    Course = new Course {
                        Id = 1,
                        Name = "Intro to Astrophysics",
                        IsActive = true,
                        Date = DateTime.Now
                    },
                    Participants = new List<Participant>
                    {
                        new Participant { 
                            Id = 1,
                            Name = "Aleksandar Trajkov"
                        }
                    }
                }
            };
        }
    }
}