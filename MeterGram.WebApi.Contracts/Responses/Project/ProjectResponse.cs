namespace MeterGram.WebApi.Contracts.Responses.Project;

public class ProjectResponse
{
    public Int32 Id { get; set; }
    public String CourseName { get; set; } = null!;
    public DateTime Date { get; set; }
    public Boolean IsActive { get; set; }
}

