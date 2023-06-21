using MeterGram.Domain.Models.Base;
using System.Linq.Expressions;

namespace MeterGram.Infrastructure.Interfaces.Database.Base;

public interface IBaseRepository<T> where T : BaseModel
{
    IQueryable<T> Select();
    Task<T?> GetByIdAsync(Int32 id, CancellationToken cancellationToken = default);
    Task<Int32> InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task<Int32> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<Int32> InsertOrUpdateAsync(Expression<Func<T, Boolean>> comparer, T entity, CancellationToken cancellationToken = default);
    Task<Int32> DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
