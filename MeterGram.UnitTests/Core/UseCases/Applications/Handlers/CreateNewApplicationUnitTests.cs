using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.Interfaces.Database;
using static MeterGram.Core.UseCases.Applications.Handlers.CreateNewApplication;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Handlers
{
    public class CreateNewApplicationUnitTests
    {
        private readonly Mock<ICompanyApplicationRepository> _companyApplicationRepository; 
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IParticipantRepository> _participantRepository;

        public CreateNewApplicationUnitTests()
        {
            _companyApplicationRepository = new Mock<ICompanyApplicationRepository>();
            _courseRepository = new Mock<ICourseRepository>();
            _courseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Course());
            _participantRepository = new Mock<IParticipantRepository>();
        }

        [Fact]
        public async Task WhenCalling_CreateNewAplication_ExpectListOfCourses()
        {
            var handler = new CreateNewApplication(_courseRepository.Object, _companyApplicationRepository.Object, _participantRepository.Object);
            var command = new Command
            {
                Participants = new List<ParticipantCommand>
                {
                    new ParticipantCommand(),
                    new ParticipantCommand()
                }
            };

            var response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();

            _companyApplicationRepository.Verify(x => x.InsertAsync(It.IsAny<CompanyApplication>(), It.IsAny<CancellationToken>()), Times.Once);
            _participantRepository.Verify(x => x.InsertAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task WhenCalling_CreateNewApplicationWithoutCourseRepository_ExpectException()
        {
            Func<Task> result = async () => await new CreateNewApplication(null!, _companyApplicationRepository.Object, _participantRepository.Object).Handle(new Command(), CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCalling_CreateNewApplicationWithoutApplicationRepository_ExpectException()
        {
            Func<Task> result = async () => await new CreateNewApplication(_courseRepository.Object, null!, _participantRepository.Object).Handle(new Command(), CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task WhenCalling_CreateNewApplicationWithoutParticipantRepository_ExpectException()
        {
            Func<Task> result = async () => await new CreateNewApplication(_courseRepository.Object, _companyApplicationRepository.Object, null!).Handle(new Command(), CancellationToken.None);

            var exception = await Record.ExceptionAsync(result);
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}