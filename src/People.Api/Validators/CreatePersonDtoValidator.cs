using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using People.Api.Dtos;

namespace People.Api.Validators;

public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
{
    public CreatePersonDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.Person.NameRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.Person.NameLength);
        RuleFor(x => x.DateOfBirth)
            .NotEqual(default(DateOnly)).WithMessage(ValidationMessages.Person.DateOfBirthRequired)
            .Must(ValidationHelpers.BeValidAge).WithMessage(ValidationMessages.Person.InvalidAge);
    }
}