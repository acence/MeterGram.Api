using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.CourseService;
using MeterGram.Infrastructure.CourseService.Models;
using MeterGram.Infrastructure.CourseService.Options;
using MeterGram.Infrastructure.CourseService.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MeterGram.Infrastructure.CourseService;

public class CourseExternalService : ICourseExternalService
{
    private readonly CourseServiceOptions _options;
    private readonly ICourseHttpService _courseService;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    private readonly Regex nextUrlRegex = new Regex("<(.+)>");

    public CourseExternalService(ICourseHttpService courseService, IOptions<CourseServiceOptions> options, IMemoryCache memoryCache, IMapper mapper)
    {
        _options = options.Value;
        _courseService = courseService;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<IList<Course>> GetCoursesAsync(Boolean updateCourses, CancellationToken cancellationToken)
    {
        var token = await GetAccessToken(cancellationToken);

        var result = new List<Course>();
        var uri = new Uri(new Uri(_options.BaseUrl), $"{_options.CoursesEndpoint}?isDataUpdated={JsonSerializer.Serialize(updateCourses)}");

        while(true)
        {
            var responseObject = await _courseService.GetCoursesAsync(uri, token, cancellationToken);

            foreach(var contract in responseObject.Data)
            {
                result.Add(_mapper.Map<Course>(contract));
            }
            if(!String.IsNullOrEmpty(responseObject.NextPageLink))
            {
                var link = nextUrlRegex.Match(responseObject.NextPageLink).Groups[1].Value;
                link += (link.Contains("?") ? "&" : "?") + $"isDataUpdated={JsonSerializer.Serialize(updateCourses)}";
                uri = new Uri(link);
            }
            else
            {
                break;
            }
        }

        return result;
    }

    private async Task<String> GetAccessToken(CancellationToken cancellationToken)
    {
        var token = _memoryCache.Get<String>("AccessToken");
        if(string.IsNullOrEmpty(token))
        {
            token = await _courseService.GetTokenAsync(cancellationToken);
        }

        _memoryCache.Set("AccessToken", token, TimeSpan.FromMinutes(20).Add(-TimeSpan.FromSeconds(30)));

        return token;
    }
}