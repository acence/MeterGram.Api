using MeterGram.Infrastructure.ProjectService.Models;

namespace MeterGram.Infrastructure.ProjectService.Services.Interfaces;

public interface ICourseHttpService
{
    Task<ProjectListWrapperResponseModel> GetProjectsAsync(Uri uri, String token, CancellationToken cancellationToken = default);
    Task<String> GetTokenAsync(CancellationToken cancellationToken = default);
}