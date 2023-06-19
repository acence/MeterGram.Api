using MeterGram.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace MeterGram.Infrastructure.Database.Interfaces;

public interface IDatabaseContext
{
    DbSet<T> Set<T>() where T : BaseModel;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
