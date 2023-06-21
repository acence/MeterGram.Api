using MeterGram.Domain.Models.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeterGram.Infrastructure.Database.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    public readonly IDatabaseContext _context;
    private DbSet<T>? _entities;

    public BaseRepository(IDatabaseContext context)
    {
        _context = context;
    }

    protected virtual DbSet<T> Entities
    {
        get
        {
            if (_entities == null)
                _entities = _context.Set<T>();
            return _entities;
        }
    }

    public virtual IQueryable<T> Select()
    {
        return Entities.AsNoTracking();
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<int> InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        (_context as DbContext)!.Entry(entity).State = EntityState.Added;

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<Int32> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        (_context as DbContext)!.Entry(entity).State = EntityState.Modified;

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<Int32> InsertOrUpdateAsync(Expression<Func<T, bool>> comparer, T entity, CancellationToken cancellationToken = default)
    {
        if (!Entities.Any(comparer))
        {
            return await InsertAsync(entity, cancellationToken);
        }
        else
        {
            return await UpdateAsync(entity, cancellationToken);
        }
    }

    public virtual async Task<Int32> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Entities.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
