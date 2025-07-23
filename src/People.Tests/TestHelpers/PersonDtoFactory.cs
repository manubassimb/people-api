using People.Api.ApiModels;
using People.Api.Dtos;

namespace People.Tests.TestHelpers;

public static class PersonDtoFactory
{
    public static CreatePersonDto ValidCreatePersonDto()
    {
        return new CreatePersonDto(
            "Valid Name",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }

    public static CreatePersonDto CreatePersonDtoWithDefaultDate()
    {
        return new CreatePersonDto(
            "Valid Name", default
        );
    }


    public static CreatePersonDto CreatePersonDtoWithEmptyName()
    {
        return new CreatePersonDto(
            "",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }

    public static CreatePersonDto CreatePersonDtoWithFutureDateOfBirth()
    {
        return new CreatePersonDto(
            "Valid Name",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        );
    }
    
    public static CreatePersonDto CreatePersonDtoWithLongName()
    {
        return new CreatePersonDto(
            new string('A', 101),
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }
        
    public static UpdatePersonDto ValidUpdatePersonDto()
    {
        return new UpdatePersonDto(
            "Valid Name",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }

    public static UpdatePersonDto UpdatePersonDtoWithEmptyName()
    {
        return new UpdatePersonDto(
            "",
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }

    public static UpdatePersonDto UpdatePersonDtoWithFutureDateOfBirth()
    {
        return new UpdatePersonDto(
            "Valid Name",
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        );
    }

    public static UpdatePersonDto UpdatePersonDtoWithDefaultDate()
    {
        return new UpdatePersonDto(
            "Valid Name", default
        );
    }

    public static UpdatePersonDto UpdatePersonDtoWithLongName()
    {
        return new UpdatePersonDto(
            new string('A', 101),
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))
        );
    }
}