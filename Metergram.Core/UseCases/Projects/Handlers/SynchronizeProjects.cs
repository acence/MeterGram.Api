using MediatR;
using MeterGram.Infrastructure.Interfaces.Database;
using MeterGram.Infrastructure.Interfaces.ProjectService;

namespace Metergram.Core.UseCases.Projects.Handlers
{
    public class SynchronizeProjects : IRequestHandler<SynchronizeProjects.Query>
    {
        private readonly IProjectExternalService _projectExternalService;
        private readonly IProjectRepository _projectRepository;

        public SynchronizeProjects(IProjectExternalService projectExternalService, IProjectRepository projectRepository)
        {
            _projectExternalService = projectExternalService;
            _projectRepository = projectRepository;
        }
        public async Task Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await _projectExternalService.GetProjectsAsync(request.ShouldGetUpdatedData, cancellationToken);

            await _projectRepository.BulkUpsertWithIdentity(projects);
        }

        public class Query : IRequest
        {
            public Boolean ShouldGetUpdatedData { get; set; }
        }
    }
}