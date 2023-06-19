using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.ProjectService.Models;
using MeterGram.Infrastructure.ProjectService.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MeterGram.Infrastructure.ProjectService
{
    public class ProjectExternalService
    {
        private readonly ProjectServiceOptions _options;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public ProjectExternalService(IOptions<ProjectServiceOptions> options, IMemoryCache memoryCache, IMapper mapper)
        {
            _options = options.Value;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<IList<Project>> GetProjectsAsync(bool updateProjects)
        {
            var token = await GetAccessToken();

            var result = new List<Project>();
            var uri = new Uri(new Uri(_options.BaseUrl), _options.CoursesEndpoint);

            while(true)
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseObject = JsonSerializer.Deserialize<ProjectListWrapperResponseModel>(await response.Content.ReadAsStringAsync());

                foreach(var contract in responseObject.Data)
                {
                    result.Add(_mapper.Map<Project>(contract));
                }
                if(!String.IsNullOrEmpty(responseObject.NextPageLink))
                {
                    uri = new Uri(responseObject.NextPageLink))
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private async Task<string> GetAccessToken()
        {
            var token = _memoryCache.Get<string>("AccessToken");
            if(string.IsNullOrEmpty(token))
            {
                var response = await _httpClient.PostAsJsonAsync(_options.AuthEndpoint, new
                {
                    apiKey = _options.AuthSecret
                });
                if(response != null && response.IsSuccessStatusCode) 
                {
                    token =  await response.Content.ReadAsStringAsync();
                }
            }

            _memoryCache.Set("AccessToken", token, TimeSpan.FromMinutes(20).Add(-TimeSpan.FromSeconds(30)));

            return token;
        }
    }
}