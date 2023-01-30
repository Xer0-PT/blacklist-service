using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BlackList.Api.Contracts;
using BlackList.Api.IntegrationTests.Fixtures;
using BlackList.Application.Dtos;
using BlackList.Persistence.Data;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace BlackList.Api.IntegrationTests.Controllers;

// [Collection(SharedTestCollection.Name)]
public class PlayerControllerTests : IClassFixture<CustomWebApplicationFactory<Program, BlackListServiceDbContext>>
{
    private readonly CustomWebApplicationFactory<Program, BlackListServiceDbContext> _factory;
    // private readonly WireMockFixture _wireMockFixture;

    public PlayerControllerTests(CustomWebApplicationFactory<Program, BlackListServiceDbContext> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void PlayerController_GetAllWithCorrectUser_ReturnsAllPlayers()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = UserFixture.CreateUser();
        await _factory.InsertUserAsync(user);

        var players = PlayerFixture.CreatePlayerRange(10, user.Id);
        await _factory.InsertPlayerAsync(players);

        // Act
        var response = await client.GetFromJsonAsync<IReadOnlyList<PlayerDto>>($"/api/player?userFaceitId={user.FaceitId}");

        // Assert
        response.Should().NotBeNull();
        response!.Count.Should().Be(10);
        
        for (var i = 0; i < players.Length; i++)
        {
            response[i].Nickname.Should().Be(players[i].Nickname);
        }
    }
    
    [Fact]
    public async void PlayerController_GetAllWithIncorrectUser_RespondsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var userFaceitId = Guid.NewGuid();

        // Act
        var response = await client.GetAsync($"/api/player?userFaceitId={userFaceitId}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void PlayerController_CreateWithCorrectUser_CreatesPlayer()
    {
        // Arrange
        const string playerNickname = "player";
        // var client = _factory.CreateClient();
        var user = UserFixture.CreateUser();
        await _factory.InsertUserAsync(user);
        var query = new CreatePlayerQuery { PlayerNickname = "player", UserFaceitId = user.FaceitId };
        
        // _wireMockFixture.GetRequest(HttpStatusCode.OK, playerNickname);

        var wireMockSvr = WireMockServer.Start();
        
        var httpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("FaceitApiConfig:Url", wireMockSvr.Url);
            })
            .CreateClient();
        
        var request = Request.Create()
            .WithPath("/players")
            .UsingGet()
            .WithParam("nickname", playerNickname);
        
        var faceitApiResponse = new FaceitUserDetails { PlayerId = Guid.NewGuid() };
        var mockResponse = Response.Create()
            .WithBody(JsonSerializer.Serialize(faceitApiResponse))
            .WithHeader("Content-Type", "application/json")
            .WithStatusCode(HttpStatusCode.OK);
        
        wireMockSvr
            .Given(request)
            .RespondWith(mockResponse);

        // Act
        var response = await httpClient.PostAsJsonAsync($"/api/player?userFaceitId={user.FaceitId}", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
    }
    
    [Fact]
    public async void PlayerController_CreateWithIncorrecttUser_RespondsNotFound()
    {
        // Arrange
        const string playerNickname = "player";
        // var client = _factory.CreateClient();
        var userFaceitId = Guid.NewGuid();
        var query = new CreatePlayerQuery { PlayerNickname = "player", UserFaceitId = userFaceitId };
        
        var wireMockSvr = WireMockServer.Start();
        
        var httpClient = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("FaceitApiConfig:Url", wireMockSvr.Url);
            })
            .CreateClient();
        
        var request = Request.Create()
            .WithPath("/players")
            .UsingGet()
            .WithParam("nickname", playerNickname);
        
        var faceitApiResponse = new FaceitUserDetails { PlayerId = Guid.NewGuid() };
        var mockResponse = Response.Create()
            .WithBody(JsonSerializer.Serialize(faceitApiResponse))
            .WithHeader("Content-Type", "application/json")
            .WithStatusCode(HttpStatusCode.NotFound);
        
        wireMockSvr
            .Given(request)
            .RespondWith(mockResponse);
        
        // Act
        var response = await httpClient.PostAsJsonAsync($"/api/player?userFaceitId={userFaceitId}", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}