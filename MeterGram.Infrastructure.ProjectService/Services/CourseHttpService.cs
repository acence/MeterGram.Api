using MeterGram.Infrastructure.ProjectService.Models;
using MeterGram.Infrastructure.ProjectService.Options;
using MeterGram.Infrastructure.ProjectService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

namespace MeterGram.Infrastructure.ProjectService.Services;

public class CourseHttpService : ICourseHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ProjectServiceOptions _options;

    public CourseHttpService(HttpClient httpClient, IOptions<ProjectServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<ProjectListWrapperResponseModel> GetProjectsAsync(Uri uri, String token, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<ProjectListWrapperResponseModel>(await response.Content.ReadAsStringAsync(cancellationToken))!;
    }

    public async Task<String> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(_options.AuthEndpoint, new
        {
            apiKey = _options.AuthSecret
        }, cancellationToken);

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<TokenResponseWrapper>(await response.Content.ReadAsStringAsync(cancellationToken))!.Data.AccessToken;
    }
}