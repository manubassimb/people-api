using FluentAssertions;
using People.Data.Entities;
using People.Tests.TestHelpers;
using People.Tests.Utilities;
using System.Net;
using System.Text;
using System.Text.Json;

namespace People.Tests.Endpoints;

public class PeopleApiCrudTests : IClassFixture<PeopleApiTestHost>
{
    private readonly HttpClient _client;

    public PeopleApiCrudTests(PeopleApiTestHost host)
    {
        _client = host.CreateClient();
    }

    [Fact]
    public async Task CreatePerson_ReturnsCreated()
    {
        // Arrange
        var person = PersonDtoFactory.ValidCreatePersonDto();

        var content = CreateJsonContent(person);
        // Act
        var response = await _client.PostAsync("/people", content);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseContent = await response.Content.ReadAsStringAsync();

        responseContent.Should().Contain("Valid Name");
    }

    [Fact]
    public async Task GetPeople_ReturnsCreatedPersonInList()
    {
        // Arrange
        var person = PersonDtoFactory.ValidCreatePersonDto();
        var createResponse = await _client.PostAsync("/people", CreateJsonContent(person));
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Act
        var response = await _client.GetAsync("/people");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(person.Name);
    }

    [Fact]
    public async Task UpdatePerson_ReturnsOk()
    {
        // Arrange
        var createPerson = PersonDtoFactory.ValidCreatePersonDto();

        var createResponse = await _client.PostAsync("/people", CreateJsonContent(createPerson));
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdPersonJson = await createResponse.Content.ReadAsStringAsync();
        var createdPerson = JsonSerializer.Deserialize<Person>(createdPersonJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        int personId = createdPerson?.Id ?? 0;
        personId.Should().NotBe(0, "Person Id must be assigned after creation");

        var updatedPersonDto = PersonDtoFactory.ValidUpdatePersonDto();

        // Act
        var updateResponse = await _client.PutAsync($"/people/{personId}", CreateJsonContent(updatedPersonDto));

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updatedContent = await updateResponse.Content.ReadAsStringAsync();
        updatedContent.Should().Contain(updatedPersonDto.Name);
    }

    [Fact]
    public async Task DeletePerson_ReturnsNoContent()
    {
        // Arrange        
        var createPerson = PersonDtoFactory.ValidCreatePersonDto();
        var createResponse = await _client.PostAsync("/people", CreateJsonContent(createPerson));
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdPersonJson = await createResponse.Content.ReadAsStringAsync();
        var createdPerson = JsonSerializer.Deserialize<Person>(createdPersonJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        int personId = createdPerson?.Id ?? 0;
        personId.Should().NotBe(0);

        // Act
        var deleteResponse = await _client.DeleteAsync($"/people/{personId}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
               
        var listResponse = await _client.GetAsync("/people");
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var listContent = await listResponse.Content.ReadAsStringAsync();
        var people = JsonSerializer.Deserialize<List<Person>>(listContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        people.Should().NotContain(p => p.Id == personId);
    }

    private StringContent CreateJsonContent<T>(T obj)
    {
        return new StringContent(
            JsonSerializer.Serialize(obj),
            Encoding.UTF8,
            "application/json");
    }
}
