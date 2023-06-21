using FluentValidation;
using FluentValidation.Validators;
using Metergram.Core.Behaviours;
using Metergram.Core.UseCases.Applications.UseCases;
using System.Text.RegularExpressions;

namespace Metergram.Core.UseCases.Applications.Validators
{
    public class IsCreateNewApplicationParticipantsValid : AbstractValidator<CreateNewApplication.ParticipantCommand>
    {
        public IsCreateNewApplicationParticipantsValid()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Continue)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Continue)
                .MaximumLength(50)
                .Must(IsCreateNewApplicationValid.PhoneRegex.IsMatch)
                .WithMessage("Phone can only contain numbers, and the following signs: +, -, (, ) or empty spaces")
                .WithErrorCode(ValidationErrorCodes.NotValidContent);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Continue)
                .MaximumLength(100)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        }
    }
}