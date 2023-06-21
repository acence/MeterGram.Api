using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database.Base;

namespace MeterGram.Infrastructure.Interfaces.Database;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<IList<Project>> GetAllProjectsAsync(bool onlyActive, CancellationToken cancellationToken = default);

    Task<Boolean> DoesProjectExistAndIsActive(int projectId, CancellationToken cancellationToken = default);

    Task BulkUpsertWithIdentity(IList<Project> projects, CancellationToken cancellationToken = default);
}
