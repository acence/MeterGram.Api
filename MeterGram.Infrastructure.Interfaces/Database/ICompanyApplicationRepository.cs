using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database.Base;

namespace MeterGram.Infrastructure.Interfaces.Database;

public interface ICompanyApplicationRepository : IBaseRepository<CompanyApplication>
{
    Task<(int TotalItems, IList<CompanyApplication> Data)> GetAllAsync(int pageNumber, int pageSize, string? name = null, string? email = null, string? phone = null, CancellationToken cancellationToken = default);
    Task<(int TotalItems, IList<CompanyApplication> Data)> GetAllForParticipantAsync(int pageNumber, int pageSize, string? name = null, string? email = null, string? phone = null, CancellationToken cancellationToken = default);
}