using MediatR;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Applications.Handlers;

public class CreateNewApplication : IRequestHandler<CreateNewApplication.Command, CompanyApplication>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICompanyApplicationRepository _companyApplicationRepository;
    private readonly IParticipantRepository _participantRepository;

    public CreateNewApplication(ICourseRepository courseRepository, ICompanyApplicationRepository companyApplicationRepository, IParticipantRepository participantRepository)
    {
        ArgumentNullException.ThrowIfNull(courseRepository);
        ArgumentNullException.ThrowIfNull(companyApplicationRepository);
        ArgumentNullException.ThrowIfNull(participantRepository);

        _courseRepository = courseRepository;
        _companyApplicationRepository = companyApplicationRepository;
        _participantRepository = participantRepository;
    }
    public async Task<CompanyApplication> Handle(Command request, CancellationToken cancellationToken)
    {
        var project = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

        var application = new CompanyApplication
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Course = project!
        };

        await _companyApplicationRepository.InsertAsync(application, cancellationToken);

        foreach(var participantRequest in request.Participants)
        {
            var participant = new Participant
            {
                CompanyApplication = application,
                Name = participantRequest.Name,
                Email = participantRequest.Email,
                Phone = participantRequest.Phone,
            };
            await _participantRepository.InsertAsync(participant, cancellationToken);
        }

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