using BlackList.Api.Controllers;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Moq;

namespace BlackList.Api.UnitTests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _service;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _service = new Mock<IUserService>();
        _controller = new UserController(_service.Object);
    }

    [Fact]
    public async void UserController_CreateUser_Creates()
    {
        // Arrange
        var nickname = "nickname";
        _service
            .Setup(x => x.CreateUserAsync(nickname, default))
            .ReturnsAsync(It.IsAny<UserDto>())
            .Verifiable();

        // Act
        await _controller.CreateUser(nickname, default);

        // Assert
        _service.Verify();
    }
}
