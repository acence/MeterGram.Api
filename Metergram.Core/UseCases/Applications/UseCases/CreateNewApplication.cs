using MediatR;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;

namespace MeterGram.Core.UseCases.Applications.UseCases;

public class CreateNewApplication : IRequestHandler<CreateNewApplication.Command, CompanyApplication>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICompanyApplicationRepository _companyApplicationRepository;

    public CreateNewApplication(IProjectRepository projectRepository, ICompanyApplicationRepository companyApplicationRepository)
    {
        _projectRepository = projectRepository;
        _companyApplicationRepository = companyApplicationRepository;
    }
    public async Task<CompanyApplication> Handle(Command request, CancellationToken cancellationToken)
    {
        var project = await  _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        var application = new CompanyApplication
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Project = project!,
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
        public String Name { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String Email { get; set; } = null!;

        public Int32 ProjectId { get; set; }

        public IList<ParticipantCommand> Participants { get; set; }
    }

    public class ParticipantCommand
    {
        public String Name { get; set; } = null!;
        public String Phone { get; set; } = null!;
        public String Email { get; set; } = null!;
    }
}