using MediatR;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Courses.Handlers;

public class GetAllCourses : IRequestHandler<GetAllCourses.Query, IList<Course>>
{
    private readonly ICourseRepository _projectRepository;

    public GetAllCourses(ICourseRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<IList<Course>> Handle(Query request, CancellationToken cancellationToken)
    {
        return await _projectRepository.GetAllCourseAsync(request.OnlyActive, cancellationToken);
    }

    public class Query: IRequest<IList<Course>>
	{
        public Boolean OnlyActive { get; set; }
    }
}
