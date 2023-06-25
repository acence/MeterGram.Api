using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace MeterGram.Infrastructure.Database.Repositories;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(IDatabaseContext context) : base(context)
    {
    }

    public async Task<IList<Course>> GetAllCourseAsync(bool onlyActive, CancellationToken cancellationToken = default)
    {
        var projects = Entities.AsQueryable();
        if(onlyActive)
        {
            projects = projects.Where(x => x.IsActive);
        }

        return await projects.ToListAsync(cancellationToken);
    }

    public async Task<Boolean> DoesCourseExistAndIsActive(int projectId, CancellationToken cancellationToken = default)
    {
        var project = await GetByIdAsync(projectId, cancellationToken);

        if(project == null || !project.IsActive)
        {
            return false;
        }
        return true;
    }

    public async Task BulkUpsertWithIdentity(IList<Course> projects, CancellationToken cancellationToken = default)
    {
        var context = (_context as DatabaseContext)!;
        using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            if (context.Database.IsRelational())
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Courses ON;", cancellationToken);
            }
            foreach(var project in projects)
            {
                if(!Entities.Any(x => x.Id == project.Id))
                {
                    context.Entry(project).State = EntityState.Added;
                }
                else
                {
                    context.Entry(project).State = EntityState.Modified;
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            if (context.Database.IsRelational())
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Courses OFF", cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
    }
}

