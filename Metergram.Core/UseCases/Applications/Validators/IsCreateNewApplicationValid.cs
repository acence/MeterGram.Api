﻿using FluentValidation;
using FluentValidation.Validators;
using MeterGram.Core.Behaviours;
using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.Infrastructure.Interfaces.Database;
using System.Text.RegularExpressions;

namespace MeterGram.Core.UseCases.Applications.Validators;

public class IsCreateNewApplicationValid : AbstractValidator<CreateNewApplication.Command>
{
    public static readonly Regex PhoneRegex = new Regex("^[ \\+()\\-.\\d]+$", RegexOptions.Compiled);

    public IsCreateNewApplicationValid(ICourseRepository projectRepository)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .MaximumLength(50)
            .Must(x => !string.IsNullOrEmpty(x) && PhoneRegex.IsMatch(x))
            .WithMessage("Phone can only contain numbers, and the following signs: +, -, (, ) or empty spaces")
            .WithErrorCode(ValidationErrorCodes.NotValidContent);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

        RuleFor(x => x.CourseId)
            .Cascade(CascadeMode.Continue)
            .GreaterThan(0)
            .MustAsync(projectRepository.DoesCourseExistAndIsActive)
            .WithMessage(x => $"Course with Id: [{x.CourseId}] is not present in system or inactive")
            .WithErrorCode(ValidationErrorCodes.NotAvailable);

        RuleFor(x => x.Participants)
            .NotEmpty();

        RuleForEach(x => x.Participants)
            .SetValidator(new IsCreateNewApplicationParticipantsValid());
    }
}