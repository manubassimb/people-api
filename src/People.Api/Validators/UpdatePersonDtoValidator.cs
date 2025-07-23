using FluentValidation;
using People.Api.Dtos;

namespace People.Api.Validators;


public class UpdatePersonDtoValidator : AbstractValidator<UpdatePersonDto>
{
    public UpdatePersonDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.Person.NameRequired)
            .MaximumLength(100)
            .WithMessage(ValidationMessages.Person.NameLength);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage(ValidationMessages.Person.DateOfBirthRequired)
            .Must(ValidationHelpers.BeValidAge)
            .WithMessage(ValidationMessages.Person.InvalidAge);
    }
}