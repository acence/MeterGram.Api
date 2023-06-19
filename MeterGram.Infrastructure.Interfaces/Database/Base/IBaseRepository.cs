using MeterGram.Domain.Models.Base;
using System.Linq.Expressions;

namespace MeterGram.Infrastructure.Interfaces.Database.Base;

public interface IBaseRepository<T> where T : BaseModel
{
    IQueryable<T> Select();
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> InsertAsync(T entity, CancellationToken cancellationToken);
    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<int> InsertOrUpdateAsync(Expression<Func<T, bool>> comparer, T entity, CancellationToken cancellationToken);
    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken);
}
