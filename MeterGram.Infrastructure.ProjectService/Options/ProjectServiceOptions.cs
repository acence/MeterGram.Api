namespace MeterGram.Infrastructure.ProjectService.Options;

public class ProjectServiceOptions
{
    public String BaseUrl { get; set; } = null!;
    public String AuthEndpoint { get; set; } = null!;
    public String CoursesEndpoint { get; set; } = null!;
    public String AuthSecret { get; set; } = null!;
}
