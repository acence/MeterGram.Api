using FluentValidation.Results;
using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Core.UseCases.Applications.Validators;
using MeterGram.UnitTests.Core.UseCases.Applications.Validators.TheoryData;

namespace MeterGram.UnitTests.Core.UseCases.Applications.Validators;

public class IsGetAllApplicationsValidUnitTests
{
    public readonly IsGetAllApplicationsValid _validator;

    public IsGetAllApplicationsValidUnitTests()
    {
        _validator = new IsGetAllApplicationsValid();
    }

    [Theory]
    [ClassData(typeof(GetAllApplicationsValidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithValidData_ExpectNoError(GetAllApplications.Query query)
    {
        var result = await _validator.ValidateAsync(query);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(GetAllApplicationsInvalidData))]
    public async Task WhenValidatingCreateNewCarCommand_WithInvalidData_ExpectError(GetAllApplications.Query query, IList<(string property, string errorCode)> expectedErrors)
    {
        ValidationResult result = await _validator.ValidateAsync(query);

        // Assert
        IEnumerable<(string, string)> validationErrors = result.Errors.Select(x => (x.PropertyName, x.ErrorCode));
        validationErrors.Should().BeEquivalentTo(expectedErrors);
    }
}