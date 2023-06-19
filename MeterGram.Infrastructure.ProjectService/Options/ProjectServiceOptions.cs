namespace MeterGram.Infrastructure.ProjectService.Options;

public class ProjectServiceOptions
{
    public string BaseUrl { get; set; } = null!;
    public string AuthEndpoint { get; set; } = null!;
    public string CoursesEndpoint { get; set; } = null!;
    public string AuthSecret { get; set; } = null!;
}
