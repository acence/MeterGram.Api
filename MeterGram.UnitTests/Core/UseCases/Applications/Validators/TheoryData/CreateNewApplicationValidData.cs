using MeterGram.Core.UseCases.Applications.Handlers;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

public class CreateNewApplicationValidData : TheoryData<CreateNewApplication.Command>
{
    public CreateNewApplicationValidData()
    {
        Add(new CreateNewApplication.Command { Name = "Test Name", Email = "a@a.com", Phone = "123", CourseId = 1, Participants = new List<CreateNewApplication.ParticipantCommand> { new CreateNewApplication.ParticipantCommand { Name = "Test Name", Email = "a@a.com", Phone = "123" } } });
    }
}