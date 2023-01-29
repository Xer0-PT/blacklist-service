using BlackList.Api.Contracts;
using BlackList.Api.Controllers;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Moq;

namespace BlackList.Api.UnitTests.Controllers;

public class PlayerControllerTests
{
    private readonly Mock<IPlayerService> _service;
    private readonly PlayerController _controller;

    public PlayerControllerTests()
    {
        _service = new Mock<IPlayerService>();
        _controller = new PlayerController(_service.Object);
    }

    [Fact]
    public async void PlayerController_GetAll_CallsCorrectMethod()
    {
        // Arrange
        var userId = new Guid();
        _service
            .Setup(x => x.GetAllPlayersAsync(userId, default))
            .ReturnsAsync(It.IsAny<IReadOnlyList<PlayerDto>>())
            .Verifiable();

        // Act
        await _controller.GetAll(userId, default);

        // Assert
        _service.Verify();
    }

    [Fact]
    public async void PlayerController_Create_CallsCorrectMethod()
    {
        // Arrange
        var userId = new Guid();
        var playerNickname = "nickname";
        var query = new CreatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = userId };

        _service
            .Setup(x => x.CreatePlayerAsync(userId, playerNickname, default))
            .ReturnsAsync(It.IsAny<PlayerDto>())
            .Verifiable();

        // Act
        await _controller.Create(query, default);

        // Assert
        _service.Verify();
    }
    
    [Fact]
    public async void PlayerController_Update_CallsCorrectMethod()
    {
        // Arrange
        var userId = new Guid();
        var playerNickname = "nickname";
        var query = new UpdatePlayerQuery { PlayerNickname = playerNickname, UserFaceitId = userId };

        _service
            .Setup(x => x.UndoPlayerBanAsync(userId, playerNickname, default))
            .ReturnsAsync(It.IsAny<PlayerDto>())
            .Verifiable();

        // Act
        await _controller.Update(query, default);

        // Assert
        _service.Verify();
    }
}