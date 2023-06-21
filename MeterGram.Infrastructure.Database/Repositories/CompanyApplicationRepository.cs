using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Infrastructure.Database.Repositories
{
    public class CompanyApplicationRepository : BaseRepository<CompanyApplication>, ICompanyApplicationRepository
    {
        public CompanyApplicationRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}