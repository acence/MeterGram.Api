using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database.Base;

namespace MeterGram.Infrastructure.Interfaces.Database;

public interface ICourseRepository : IBaseRepository<Course>
{
    Task<IList<Course>> GetAllCourseAsync(bool onlyActive, CancellationToken cancellationToken = default);

    Task<Boolean> DoesCourseExistAndIsActive(int projectId, CancellationToken cancellationToken = default);

    Task BulkUpsertWithIdentity(IList<Course> courses, CancellationToken cancellationToken = default);
}
