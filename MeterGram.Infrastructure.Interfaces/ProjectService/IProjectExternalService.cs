using MeterGram.Domain.Models;

namespace MeterGram.Infrastructure.Interfaces.ProjectService;

public interface IProjectExternalService
{
    Task<IList<Project>> GetProjectsAsync(bool updateProjects, CancellationToken cancellationToken);
}
