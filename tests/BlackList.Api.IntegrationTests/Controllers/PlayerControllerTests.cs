using BlackList.Api.IntegrationTests.Fixtures;
using BlackList.Persistence.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlackList.Api.IntegrationTests.Controllers;

// [Collection(SharedTestCollection.Name)]
public class PlayerControllerTests : IClassFixture<CustomWebApplicationFactory<Program, BlackListServiceDbContext>>
{
    private readonly CustomWebApplicationFactory<Program, BlackListServiceDbContext> _factory;
    private readonly HttpClient _client;

    public PlayerControllerTests(CustomWebApplicationFactory<Program, BlackListServiceDbContext> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async void BlackListedPlayerController_GetAll_ReturnsAll()
    {
        // Arrange
        var userId = new Guid();

        // Act
        var response = await _client.GetAsync($"/api/player?userFaceitId={userId}", CancellationToken.None);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}