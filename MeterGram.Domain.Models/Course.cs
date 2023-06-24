using MeterGram.Domain.Models.Base;

namespace MeterGram.Domain.Models;

public class Course : BaseModel
{
    public String Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public Boolean IsActive { get; set; }

    public ICollection<CompanyApplication> CompanyApplications { get; set; } = null!;
}
