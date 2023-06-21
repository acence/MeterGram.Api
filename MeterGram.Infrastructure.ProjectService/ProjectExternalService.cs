using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.ProjectService;
using MeterGram.Infrastructure.ProjectService.Models;
using MeterGram.Infrastructure.ProjectService.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MeterGram.Infrastructure.ProjectService
{
    public class ProjectExternalService : IProjectExternalService
    {
        private readonly ProjectServiceOptions _options;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        private readonly Regex nextUrlRegex = new Regex("<(.+)>");

        public ProjectExternalService(IOptions<ProjectServiceOptions> options, IMemoryCache memoryCache, IMapper mapper)
        {
            _options = options.Value;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<IList<Project>> GetProjectsAsync(Boolean updateProjects, CancellationToken cancellationToken)
        {
            var token = await GetAccessToken(cancellationToken);

            var result = new List<Project>();
            var uri = new Uri(new Uri(_options.BaseUrl), $"{_options.CoursesEndpoint}?isDataUpdated={JsonSerializer.Serialize(updateProjects)}");

            while(true)
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseObject = JsonSerializer.Deserialize<ProjectListWrapperResponseModel>(await response.Content.ReadAsStringAsync());

                foreach(var contract in responseObject.Data)
                {
                    result.Add(_mapper.Map<Project>(contract));
                }
                if(!String.IsNullOrEmpty(responseObject.NextPageLink))
                {
                    var link = nextUrlRegex.Match(responseObject.NextPageLink).Groups[1].Value;
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
                var response = await _httpClient.PostAsJsonAsync(_options.AuthEndpoint, new
                {
                    apiKey = _options.AuthSecret
                }, cancellationToken);

                response.EnsureSuccessStatusCode();

                token = JsonSerializer.Deserialize<TokenResponseWrapper>(await response.Content.ReadAsStringAsync(cancellationToken))!.Data.AccessToken;
            }

            _memoryCache.Set("AccessToken", token, TimeSpan.FromMinutes(20).Add(-TimeSpan.FromSeconds(30)));

            return token;
        }
    }
}