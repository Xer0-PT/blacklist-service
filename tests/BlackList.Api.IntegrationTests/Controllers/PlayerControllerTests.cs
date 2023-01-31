using System.Net;
using System.Net.Http.Json;
using BlackList.Api.Contracts;
using BlackList.Api.IntegrationTests.Fixtures;
using BlackList.Application.Dtos;
using FluentAssertions;

namespace BlackList.Api.IntegrationTests.Controllers;

[Collection(SharedTestCollection.Name)]
public class PlayerControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly WireMockFixture _wireMockFixture;
    private readonly DatabaseFixture _databaseFixture;

    public PlayerControllerTests(CustomWebApplicationFactory factory, WireMockFixture wireMockFixture, DatabaseFixture databaseFixture)
    {
        _factory = factory;
        _wireMockFixture = wireMockFixture;
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async void PlayerController_GetAllWithCorrectUser_ReturnsAllPlayers()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);

        var players = PlayerFixture.CreatePlayerRange(10, user.Id);
        await _databaseFixture.InsertPlayerAsync(players);

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
    public async void PlayerController_CreateWithCorrectUser_RespondsOk()
    {
        // Arrange
        const string playerNickname = "player";
        
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var query = new CreatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = user.FaceitId };
        
        _wireMockFixture.GetRequest(HttpStatusCode.OK, playerNickname);

        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async void PlayerController_CreateWithIncorrectUser_RespondsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var userFaceitId = Guid.NewGuid();
        var query = new CreatePlayerQuery { PlayerNickname = "player", UserFaceitId = userFaceitId };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async void PlayerController_CreateWithIncorrectPlayer_RespondsNotFound()
    {
        // Arrange
        const string playerNickname = "player";
        var client = _factory.CreateClient();
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var query = new CreatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = user.FaceitId };
        
        _wireMockFixture.GetRequest(HttpStatusCode.NotFound, playerNickname);
        
        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async void PlayerController_CreateWithUserBanningHimself_RespondsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var query = new CreatePlayerQuery { PlayerNickname = user.Nickname, UserFaceitId = user.FaceitId };
                
        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async void PlayerController_CreateWithUserBanningPlayerAlreadyBanned_RespondsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var player = PlayerFixture.CreatePlayer(user.Id);
        await _databaseFixture.InsertPlayerAsync(player);

        var query = new CreatePlayerQuery { PlayerNickname = player.Nickname, UserFaceitId = user.FaceitId };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async void PlayerController_CreateWithUserRebanningPlayer_RebansPlayer()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var player = PlayerFixture.CreatePlayer(userId: user.Id, banned: false);
        await _databaseFixture.InsertPlayerAsync(player);

        var query = new CreatePlayerQuery { PlayerNickname = player.Nickname, UserFaceitId = user.FaceitId };
        
        // Act
        var response = await client.PostAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async void PlayerController_UpdateWithCorrectUserAndPlayer_RespondsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        var player = PlayerFixture.CreatePlayer(user.Id);
        await _databaseFixture.InsertPlayerAsync(player);

        var query = new UpdatePlayerQuery { PlayerNickname = player.Nickname, UserFaceitId = user.FaceitId };
        
        // Act
        var response = await client.PutAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async void PlayerController_UpdateWithIncorrectUser_RespondsNotFound()
    {
        // Arrange
        const string playerNickname = "player";
        var client = _factory.CreateClient();
        var userFaceitId = Guid.NewGuid();
        
        var query = new UpdatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = userFaceitId };

        // Act
        var response = await client.PutAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async void PlayerController_UpdateWithIncorrectPlayer_RespondsNotFound()
    {
        // Arrange
        const string playerNickname = "player";
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);

        var query = new UpdatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = user.FaceitId };

        // Act
        var response = await client.PutAsJsonAsync("/api/player", query);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}