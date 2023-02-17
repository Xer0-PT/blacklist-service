using System.Net;
using BlackList.Api.IntegrationTests.Fixtures;
using FluentAssertions;

namespace BlackList.Api.IntegrationTests.Controllers;

[Collection(SharedTestCollection.Name)]
public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly WireMockFixture _wireMockFixture;
    private readonly DatabaseFixture _databaseFixture;

    public UserControllerTests(CustomWebApplicationFactory factory, WireMockFixture wireMockFixture, DatabaseFixture databaseFixture)
    {
        _factory = factory;
        _wireMockFixture = wireMockFixture;
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async void UserController_CreateUser_RespondsOk()
    {
        // Arrange
        const string userNickname = "user";
        var client = _factory.CreateClient();
        
        _wireMockFixture.GetRequest(HttpStatusCode.OK, userNickname);

        // Act
        var response = await client.PostAsJsonAsync("/api/user", userNickname);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async void UserController_CreateExistingtUser_RespondsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var user = UserFixture.CreateUser();
        await _databaseFixture.InsertUserAsync(user);
        
        _wireMockFixture.GetRequest(HttpStatusCode.OK, user.Nickname, user.FaceitId);

        // Act
        var response = await client.PostAsJsonAsync("/api/user", user.Nickname);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async void UserController_CreateWithIncorrectUser_RespondsNotFound()
    {
        // Arrange
        const string userNickname = "user";
        var client = _factory.CreateClient();
        
        _wireMockFixture.GetRequest(HttpStatusCode.NotFound, userNickname);

        // Act
        var response = await client.PostAsJsonAsync("/api/user", userNickname);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}