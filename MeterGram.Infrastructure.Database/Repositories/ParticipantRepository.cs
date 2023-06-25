using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Database.Base;
using MeterGram.Infrastructure.Database.Interfaces;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Infrastructure.Database.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}