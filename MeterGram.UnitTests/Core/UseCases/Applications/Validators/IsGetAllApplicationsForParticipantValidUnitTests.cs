using FluentValidation.Results;
using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Core.UseCases.Applications.Validators;
using MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators;

public class IsGetAllApplicationsForParticipantValidUnitTests
{
    public readonly IsGetAllApplicationsForParticipanValid _validator;

    public IsGetAllApplicationsForParticipantValidUnitTests()
    {
        _validator = new IsGetAllApplicationsForParticipanValid();
    }

    [Theory]
    [ClassData(typeof(GetAllApplicationsForParticipantValidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithValidData_ExpectNoError(GetAllApplicationsForParticipant.Query query)
    {
        var result = await _validator.ValidateAsync(query);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(GetAllApplicationsForParticipantInvalidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithInvalidData_ExpectError(GetAllApplicationsForParticipant.Query query, IList<(string property, string errorCode)> expectedErrors)
    {
        ValidationResult result = await _validator.ValidateAsync(query);

        // Assert
        IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
        validationErrors.Should().BeEquivalentTo(expectedErrors);
    }
}