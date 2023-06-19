using MeterGram.Domain.Models.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeterGram.Infrastructure.Database;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    #region Overrides 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    #endregion Overrides

    #region Methods 

    public new DbSet<T> Set<T>() where T : BaseModel
    {
        return base.Set<T>();
    }

    #endregion Methods
}

