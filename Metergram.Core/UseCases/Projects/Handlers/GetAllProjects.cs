using MediatR;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Projects.Handlers;

public class GetAllProjects : IRequestHandler<GetAllProjects.Query, IList<Project>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjects(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<IList<Project>> Handle(Query request, CancellationToken cancellationToken)
    {
        return await _projectRepository.GetAllProjectsAsync(request.OnlyActive, cancellationToken);
    }

    public class Query: IRequest<IList<Project>>
	{
        public Boolean OnlyActive { get; set; }
    }
}
