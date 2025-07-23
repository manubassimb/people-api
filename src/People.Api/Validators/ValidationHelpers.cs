namespace People.Api.Validators;

public static class ValidationHelpers
{
    public static bool BeValidAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age >= 0 && age <= 120;
    }
}