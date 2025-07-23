using System.Diagnostics.CodeAnalysis;

namespace People.Api.Dtos;

[ExcludeFromCodeCoverage]
public record PersonDto(int Id, string Name, DateOnly DateOfBirth);