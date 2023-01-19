namespace BlackList.Api.UnitTests.Controllers;

using BlackList.Api.Contracts;
using BlackList.Api.Controllers;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Moq;

public class UnitTest1
{
    private readonly Mock<IBlackListedPlayerService> _service;
    private readonly BlackListedPlayerController _controller;

    public UnitTest1()
    {
        _service = new Mock<IBlackListedPlayerService>();
        _controller = new BlackListedPlayerController(_service.Object);
    }

    [Fact]
    public async void BlackListedPlayerController_GetAll_ReturnsAll()
    {
        // Arrange
        _service
            .Setup(x => x.GetAllBlackListedPlayersAsync("", default))
            .ReturnsAsync(It.IsAny<IReadOnlyList<BlackListedPlayerDto>>())
            .Verifiable();

        // Act
        var result = await _controller.GetAll("", default);

        // Assert
        _service.Verify();
    }

    [Fact]
    public async void BlackListedPlayerController_Create_Creates()
    {
        // Arrange
        _service
            .Setup(x => x.CreateBlackListedPlayerAsync("", "", default))
            .ReturnsAsync(It.IsAny<BlackListedPlayerDto>())
            .Verifiable();

        // Act
        var result = await _controller.Create(new CreateBlackListedPlayerQuery("", ""), default);

        // Assert
        _service.Verify();
    }
}