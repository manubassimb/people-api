using FluentAssertions;
using People.Tests.Utilities;
using System.Net;

namespace People.Tests.Endpoints;

public class PeopleApiHealthTests : IClassFixture<PeopleApiTestHost>
{
    private readonly HttpClient _client;

    public PeopleApiHealthTests(PeopleApiTestHost host)
    {
        _client = host.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Healthy");
    }
}