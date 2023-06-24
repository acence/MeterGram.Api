using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.ProjectService;
using MeterGram.Infrastructure.ProjectService.Models;
using MeterGram.Infrastructure.ProjectService.Options;
using MeterGram.Infrastructure.ProjectService.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MeterGram.Infrastructure.ProjectService;

public class ProjectExternalService : IProjectExternalService
{
    private readonly ProjectServiceOptions _options;
    private readonly ICourseHttpService _courseService;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    private readonly Regex nextUrlRegex = new Regex("<(.+)>");

    public ProjectExternalService(ICourseHttpService courseService, IOptions<ProjectServiceOptions> options, IMemoryCache memoryCache, IMapper mapper)
    {
        _options = options.Value;
        _courseService = courseService;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<IList<Project>> GetProjectsAsync(Boolean updateProjects, CancellationToken cancellationToken)
    {
        var token = await GetAccessToken(cancellationToken);

        var result = new List<Project>();
        var uri = new Uri(new Uri(_options.BaseUrl), $"{_options.CoursesEndpoint}?isDataUpdated={JsonSerializer.Serialize(updateProjects)}");

        while(true)
        {
            var responseObject = await _courseService.GetProjectsAsync(uri, token, cancellationToken);

            foreach(var contract in responseObject.Data)
            {
                result.Add(_mapper.Map<Project>(contract));
            }
            if(!String.IsNullOrEmpty(responseObject.NextPageLink))
            {
                var link = nextUrlRegex.Match(responseObject.NextPageLink).Groups[1].Value;
                link += (link.Contains("?") ? "&" : "?") + $"isDataUpdated={JsonSerializer.Serialize(updateProjects)}";
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