using MediatR;
using MeterGram.Infrastructure.Interfaces.Database;
using MeterGram.Infrastructure.Interfaces.CourseService;

namespace MeterGram.Core.UseCases.Courses.Handlers
{
    public class SynchronizeCourses : IRequestHandler<SynchronizeCourses.Query>
    {
        private readonly ICourseExternalService _projectExternalService;
        private readonly ICourseRepository _projectRepository;

        public SynchronizeCourses(ICourseExternalService projectExternalService, ICourseRepository projectRepository)
        {
            _projectExternalService = projectExternalService;
            _projectRepository = projectRepository;
        }
        public async Task Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await _projectExternalService.GetCoursesAsync(request.ShouldGetUpdatedData, cancellationToken);

            await _projectRepository.BulkUpsertWithIdentity(projects);
        }

        public class Query : IRequest
        {
            public Boolean ShouldGetUpdatedData { get; set; }
        }
    }
}