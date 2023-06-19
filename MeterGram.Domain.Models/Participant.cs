using MeterGram.Domain.Models.Base;

namespace MeterGram.Domain.Models;

public class Participant : BaseModel
{
    public String Name { get; set; } = null!;
    public String? Phone { get; set; }
    public String? Email { get; set; }
    public CompanyApplication CompanyApplication { get; set; } = null!;
}
