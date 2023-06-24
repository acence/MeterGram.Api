using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace MeterGram.Infrastructure.Database.Repositories
{
    public class CompanyApplicationRepository : BaseRepository<CompanyApplication>, ICompanyApplicationRepository
    {
        public CompanyApplicationRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task<(int TotalItems, IList<CompanyApplication> Data)> GetAllAsync(int pageNumber, int pageSize, string? name = null, string? email = null, string? phone = null, CancellationToken cancellationToken = default)
        {
            var query = Entities.AsQueryable();

            if (name != null) query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            if (phone != null) query = query.Where(x => x.Phone.ToLower().Contains(phone.ToLower()));
            if (email != null) query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));

            var count = await query.CountAsync(cancellationToken);

            if (pageSize > 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            query = query.Include(x => x.Project);
            query = query.Include(x => x.Participants);

            return (count, await query.ToListAsync(cancellationToken));
        }

        public async Task<(int TotalItems, IList<CompanyApplication> Data)> GetAllForParticipantAsync(int pageNumber, int pageSize, string? name = null, string? email = null, string? phone = null, CancellationToken cancellationToken = default)
        {
            var query = Entities.AsQueryable();

            if (name != null) query = query.Where(x => x.Participants.Any(y => y.Name.ToLower().Contains(name.ToLower())));
            if (phone != null) query = query.Where(x => x.Participants.Any(y => y.Phone == null || y.Phone.ToLower().Contains(phone.ToLower())));
            if (email != null) query = query.Where(x => x.Participants.Any(y => y.Email == null || y.Email.ToLower().Contains(email.ToLower())));

            var count = await query.CountAsync(cancellationToken);

            if (pageSize > 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            query = query.Include(x => x.Project);
            query = query.Include(x => x.Participants);

            return (count, await query.ToListAsync(cancellationToken));
        }
    }
}