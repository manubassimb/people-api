using System.Diagnostics.CodeAnalysis;

namespace People.Api.Dtos;

[ExcludeFromCodeCoverage]
public record CreatePersonDto(string Name, DateOnly DateOfBirth);
