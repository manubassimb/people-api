using FluentValidation.TestHelper;
using People.Api.Validators;
using People.Tests.Constants;
using People.Tests.TestHelpers;
using Xunit;

namespace People.Tests.ValidatorTests;
public class UpdatePersonDtoValidatorTests
{
    private readonly UpdatePersonDtoValidator _validator;

    public UpdatePersonDtoValidatorTests()
    {
        _validator = new UpdatePersonDtoValidator();
    }

    [Fact]
    public void Should_Pass_WhenValidUpdateModel()
    {
        var model = PersonDtoFactory.ValidUpdatePersonDto();

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_ThrowError_WhenNameIsEmpty()
    {
        var model = PersonDtoFactory.UpdatePersonDtoWithEmptyName();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(Messages.NameRequired);
    }   

    [Fact]
    public void Should_ThrowError_WhenDateOfBirthIsInFuture()
    {
        var model = PersonDtoFactory.UpdatePersonDtoWithFutureDateOfBirth();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(Messages.InvalidAge);
    }

    [Fact]
    public void Should_ThrowError_WhenDateOfBirthIsDefault()
    {
        var model = PersonDtoFactory.UpdatePersonDtoWithDefaultDate();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage(Messages.DateOfBirthRequired);
    }

    [Fact]
    public void Should_ThrowError_WhenNameIsTooLong()
    {
        var model = PersonDtoFactory.UpdatePersonDtoWithLongName();

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(Messages.NameLength);
    }
}

