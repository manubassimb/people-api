using People.Api.Dtos;
using People.Api.Extensions;
using People.Data.Entities;

namespace People.Tests.ExtensionTests;

public class PersonExtensionsTests
{
    [Fact]
    public void ToEntity_ShouldMapCreatePersonDtoToPerson()
    {
        // Arrange
        var dto = new CreatePersonDto("Alice", DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)));

        // Act
        var entity = dto.ToEntity();

        // Assert
        Assert.Equal(dto.Name, entity.Name);
        Assert.Equal(dto.DateOfBirth, entity.DateOfBirth);
    }

    [Fact]
    public void UpdateFromDto_ShouldUpdatePersonFields()
    {
        // Arrange
        var person = new Person { Name = "Old", DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-40)) };
        var updateDto = new UpdatePersonDto("New", DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)));

        // Act
        person.UpdateFromDto(updateDto);

        // Assert
        Assert.Equal(updateDto.Name, person.Name);
        Assert.Equal(updateDto.DateOfBirth, person.DateOfBirth);
    }

    [Fact]
    public void ToDto_ShouldMapPersonToDto()
    {
        // Arrange
        var person = new Person
        {
            Id = 5,
            Name = "Bob",
            DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-35))
        };

        // Act
        var dto = person.ToDto();

        // Assert
        Assert.Equal(person.Id, dto.Id);
        Assert.Equal(person.Name, dto.Name);
        Assert.Equal(person.DateOfBirth, dto.DateOfBirth);
    }
}