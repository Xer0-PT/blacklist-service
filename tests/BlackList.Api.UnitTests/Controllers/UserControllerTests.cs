namespace BlackList.Api.UnitTests.Controllers;

using BlackList.Api.Controllers;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Moq;

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
        _service
            .Setup(x => x.CreateUserAsync(default))
            .ReturnsAsync(It.IsAny<UserDto>())
            .Verifiable();

        // Act
        var result = await _controller.CreateUser(default);

        // Assert
        _service.Verify();
    }
}
