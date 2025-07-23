using FluentValidation.TestHelper;
using People.Api.Validators;
using People.Tests.Constants;
using People.Tests.TestHelpers;

namespace People.Tests.ValidatorTests;

public class CreatePersonDtoValidatorTests
{
    private readonly CreatePersonDtoValidator _validator = new();

    public CreatePersonDtoValidatorTests()
    {
        _validator = new CreatePersonDtoValidator();
    }

    [Fact]
    public void Should_Pass_When_ModelIsValid()
    {
        var model = PersonDtoFactory.ValidCreatePersonDto();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_ThrowError_WhenDateOfBirthIsInFuture()
    {
        var model = PersonDtoFactory.CreatePersonDtoWithFutureDateOfBirth();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(Messages.InvalidAge);
    }

    [Fact]
    public void Should_ThrowError_WhenNameIsEmpty()
    {
        var model = PersonDtoFactory.CreatePersonDtoWithEmptyName();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(Messages.NameRequired);
    }

    [Fact]
    public void Should_ThrowError_WhenDateOfBirthIsDefault()
    {
        var model = PersonDtoFactory.CreatePersonDtoWithDefaultDate();
        
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(Messages.DateOfBirthRequired);
    }

    [Fact]
    public void Should_ThrowError_WhenNameIsTooLong()
    {
        var model = PersonDtoFactory.CreatePersonDtoWithLongName();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(Messages.NameLength);
    }
}
