using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database.Base;

namespace MeterGram.Infrastructure.Interfaces.Database
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
    }
}