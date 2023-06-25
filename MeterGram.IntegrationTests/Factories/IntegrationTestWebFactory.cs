using MeterGram.Infrastructure.Database;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.IntegrationTests.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

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