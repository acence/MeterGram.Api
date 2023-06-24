using MeterGram.Infrastructure.CourseService.Models;
using MeterGram.Infrastructure.CourseService.Options;
using MeterGram.Infrastructure.CourseService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

namespace MeterGram.Infrastructure.CourseService.Services;

public class CourseHttpService : ICourseHttpService
{
    private readonly HttpClient _httpClient;
    private readonly CourseServiceOptions _options;

    public CourseHttpService(HttpClient httpClient, IOptions<CourseServiceOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<CourseListWrapperResponseModel> GetCoursesAsync(Uri uri, String token, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<CourseListWrapperResponseModel>(await response.Content.ReadAsStringAsync(cancellationToken))!;
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