using System.Net;
using System.Text.Json;
using BlackList.Application.Abstractions;
using BlackList.Application.Services;
using FluentAssertions;
using Moq;

namespace BlackList.Application.UnitTests.Services;

public class FaceitGatewayTests
{
    private readonly Mock<IFaceitApi> _faceitApiMock;

    private readonly FaceitGateway _target;

    public FaceitGatewayTests()
    {
        _faceitApiMock = new Mock<IFaceitApi>();
        _target = new FaceitGateway(_faceitApiMock.Object);
    }

    [Fact]
    public async Task GetFaceitIdAsync_WhenIsSuccessStatusCode_ReturnsPlayerId()
    {
        // Arrange
        const string nickname = "test";
        var guid = Guid.NewGuid();
        var faceitUserDetails = JsonSerializer.Serialize(new { player_id = guid });
        var faceitApiResponse = new HttpResponseMessage(HttpStatusCode.OK);
        faceitApiResponse.Content = new StringContent(faceitUserDetails, System.Text.Encoding.UTF8, "application/json");
        
        _faceitApiMock
            .Setup(x => x.GetUserDetailsAsync(nickname, default))
            .ReturnsAsync(faceitApiResponse);

        // Act
         var response = await _target.GetFaceitIdAsync(nickname, default);

        // Assert
        response.Should().Be(guid);
    }

    [Fact]
    public async Task GetFaceitIdAsync_WhenIsNotSuccessStatusCode_ThrowsException()
    {
        // Arrange
        const string nickname = "test";
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        _faceitApiMock
            .Setup(x => x.GetUserDetailsAsync(nickname, default))
            .ReturnsAsync(response);

        // Act
        // Assert
        await _target.Invoking(x => x.GetFaceitIdAsync(nickname, default))
            .Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"The player {nickname} does not exist!");
    }
}