using MeterGram.Domain.Models.Base;

namespace MeterGram.Domain.Models;

public class CompanyApplication : BaseModel
{
    public String Name { get; set; } = null!;
    public String Phone { get; set; } = null!;
    public String Email { get; set; } = null!;

    public Project Project { get; set; } = null!;
    public ICollection<Participant> Participants { get; set; } = null!;
}
