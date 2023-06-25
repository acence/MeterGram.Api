using MeterGram.Infrastructure.CourseService;
using MeterGram.Infrastructure.Database;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.CourseService;
using MeterGram.IntegrationTests.SeedData;
using MeterGram.IntegrationTests.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeterGram.IntegrationTests.Factories
{
    public class IntegrationTestWebFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<DatabaseContext>(options => {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ICourseExternalService));
                if (descriptor != null) services.Remove(descriptor);

                services.AddTransient<ICourseExternalService, CourseExternalMockService>();

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var appContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>() as DatabaseContext;
                    
                    try
                    {
                        appContext!.Database.EnsureCreated();

                        foreach (var project in CourseSeedData.Get()) {
                            appContext.Entry(project).State = EntityState.Added;
                        }
                        appContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //Log errors
                        throw;
                    }
                    
                }
            });

            builder.UseEnvironment("Development");
        }
    }
}