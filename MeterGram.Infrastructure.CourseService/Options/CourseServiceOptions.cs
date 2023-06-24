namespace MeterGram.Infrastructure.CourseService.Options;

public class CourseServiceOptions
{
    public String BaseUrl { get; set; } = null!;
    public String AuthEndpoint { get; set; } = null!;
    public String CoursesEndpoint { get; set; } = null!;
    public String AuthSecret { get; set; } = null!;
}
