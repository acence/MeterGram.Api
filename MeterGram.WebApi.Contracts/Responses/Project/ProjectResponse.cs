namespace MeterGram.WebApi.Contracts.Responses.Project;

public class ProjectResponse
{
    public String CourseName { get; set; } = null!;
    public DateTime Date { get; set; }
    public Boolean IsActive { get; set; }
}

