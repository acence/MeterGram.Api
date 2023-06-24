using MediatR;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Applications.Handlers;

public class CreateNewApplication : IRequestHandler<CreateNewApplication.Command, CompanyApplication>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICompanyApplicationRepository _companyApplicationRepository;

    public CreateNewApplication(ICourseRepository courseRepository, ICompanyApplicationRepository companyApplicationRepository)
    {
        ArgumentNullException.ThrowIfNull(courseRepository);
        ArgumentNullException.ThrowIfNull(companyApplicationRepository);

        _courseRepository = courseRepository;
        _companyApplicationRepository = companyApplicationRepository;
    }
    public async Task<CompanyApplication> Handle(Command request, CancellationToken cancellationToken)
    {
        var project = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

        var application = new CompanyApplication
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Course = project!,
            Participants = request.Participants.Select(x => new Participant
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
            }).ToList(),
        };

        await _companyApplicationRepository.InsertAsync(application, cancellationToken);

        return application;
    }

    public class Command : IRequest<CompanyApplication>
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int CourseId { get; set; }

        public IList<ParticipantCommand> Participants { get; set; }
    }

    public class ParticipantCommand
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}