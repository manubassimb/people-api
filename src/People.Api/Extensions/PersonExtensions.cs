using People.Api.Dtos;
using People.Data.Entities;

namespace People.Api.Extensions;

public static class PersonExtensions
{
    public static Person ToEntity(this CreatePersonDto dto)
    {
        return new Person
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };
    }

    public static void UpdateFromDto(this Person person, UpdatePersonDto dto)
    {
        person.Name = dto.Name;
        person.DateOfBirth = dto.DateOfBirth;
    }

    public static Person ToDto(this Person person)
    {
        return new Person
        {
            Id = person.Id,
            Name = person.Name,
            DateOfBirth = person.DateOfBirth
        };
    }
}