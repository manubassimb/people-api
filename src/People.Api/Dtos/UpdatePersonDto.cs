using System.Diagnostics.CodeAnalysis;

namespace People.Api.Dtos;

[ExcludeFromCodeCoverage]
public record UpdatePersonDto(string Name, DateOnly DateOfBirth);