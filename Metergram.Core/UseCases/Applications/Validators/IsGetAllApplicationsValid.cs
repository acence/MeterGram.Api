using FluentValidation;
using FluentValidation.Validators;
using MeterGram.Core.Behaviours;
using MeterGram.Core.UseCases.Applications.UseCases;

namespace MeterGram.Core.UseCases.Applications.Validators;

public class IsGetAllApplicationsValid : AbstractValidator<GetAllApplications.Query>
{
    public IsGetAllApplicationsValid()
    {
        RuleFor(x => x.Name)
           .Cascade(CascadeMode.Continue)
           .MaximumLength(250);

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Continue)
            .MaximumLength(50)
            .Must(x => string.IsNullOrEmpty(x) || IsCreateNewApplicationValid.PhoneRegex.IsMatch(x))
            .WithMessage("Phone can only contain numbers, and the following signs: +, -, (, ) or empty spaces")
            .WithErrorCode(ValidationErrorCodes.NotValidContent);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Continue)
            .MaximumLength(100)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

        RuleFor(x => x.PageNumber)
            .Cascade(CascadeMode.Continue)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .Cascade(CascadeMode.Continue)
            .GreaterThanOrEqualTo(0);
    }
}