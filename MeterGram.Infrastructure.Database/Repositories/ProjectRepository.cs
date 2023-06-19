using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace MeterGram.Infrastructure.Database.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    private readonly IMapper _mapper;

    public ProjectRepository(IDatabaseContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<IList<Project>> GetAllProjectsAsync(bool onlyActive, CancellationToken cancellationToken)
    {
        var projects = Entities.AsQueryable();
        if(onlyActive)
        {
            projects = projects.Where(x => x.IsActive);
        }

        return await projects.ToListAsync(cancellationToken);
    }
}

