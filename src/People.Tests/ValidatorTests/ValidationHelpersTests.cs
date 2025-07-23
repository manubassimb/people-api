using People.Api.Validators;

namespace People.Tests.ValidatorTests;

public class ValidationHelpersTests
{
    [Theory]
    [InlineData(-1, false)]  // less than 0 age
    [InlineData(0, true)]    // exactly 0
    [InlineData(1, true)]    // valid
    [InlineData(120, true)]  // valid max
    [InlineData(121, false)] // above max
    public void BeValidAge_ShouldValidateAgeCorrectly(int yearsAgo, bool expected)
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-yearsAgo));

        // Act
        var result = ValidationHelpers.BeValidAge(date);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BeValidAge_ShouldFail_WhenDateIsInFuture()
    {
        // Arrange
        var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

        // Act
        var result = ValidationHelpers.BeValidAge(futureDate);

        // Assert
        Assert.False(result);
    }
}