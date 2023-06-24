using FluentValidation.Results;
using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Core.UseCases.Applications.Validators;
using MeterGram.Infrastructure.Interfaces.Database;
using MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators;

public class IsCreateNewApplicationValidUnitTests
{
    public readonly Mock<ICourseRepository> _courseRepository;
    public readonly IsCreateNewApplicationValid _validator;

    public IsCreateNewApplicationValidUnitTests()
    {
        _courseRepository = new Mock<ICourseRepository>();
        _courseRepository.Setup(x => x.DoesCourseExistAndIsActive(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _validator = new IsCreateNewApplicationValid(_courseRepository.Object);
    }

    [Theory]
    [ClassData(typeof(CreateNewApplicationValidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithValidData_ExpectNoError(CreateNewApplication.Command command)
    {
        var result = await _validator.ValidateAsync(command);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(CreateNewApplicationInvalidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithInvalidData_ExpectError(CreateNewApplication.Command command, IList<(string property, string errorCode)> expectedErrors)
    {
        ValidationResult result = await _validator.ValidateAsync(command);

        // Assert
        IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
        validationErrors.Should().BeEquivalentTo(expectedErrors);
    }
}