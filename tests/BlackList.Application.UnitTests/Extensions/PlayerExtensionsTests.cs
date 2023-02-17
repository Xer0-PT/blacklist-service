using BlackList.Application.Extensions;
using BlackList.Domain.Entities;
using FluentAssertions;

namespace BlackList.Application.UnitTests.Extensions;

public class PlayerExtensionsTests
{
    [Fact]
    public void PlayerIsAlreadyBanned_WhenTrue_ReturnsTrue()
    {
        // Arrange
        var player = new Player() { Banned = true };

        // Act
        var result = PlayerExtensions.PlayerIsAlreadyBanned(player);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void PlayerIsAlreadyBanned_WhenFalse_ReturnsFalse()
    {
        // Arrange
        var player = new Player() { Banned = false };

        // Act
        var result = PlayerExtensions.PlayerIsAlreadyBanned(player);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void PlayerIsAlreadyBanned_WhenPlayerIsNull_ReturnsFalse()
    {
        // Arrange
        // Act
        var result = PlayerExtensions.PlayerIsAlreadyBanned(null);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsReban_WhenTrue_ReturnsTrue()
    {
        // Arrange
        var player = new Player() { Banned = false };

        // Act
        var result = PlayerExtensions.IsReban(player);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsReban_WhenFalse_ReturnsFalse()
    {
        // Arrange
        var player = new Player() { Banned = true };

        // Act
        var result = PlayerExtensions.IsReban(player);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsReban_WhenPlayerIsNull_ReturnsFalse()
    {
        // Arrange
        // Act
        var result = PlayerExtensions.IsReban(null);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void UserIsBanningHimself_WhenTrue_ReturnsTrue()
    {
        // Arrange
        const string playerNickname = "test";
        const string userNickname = "test";

        // Act
        var result = PlayerExtensions.UserIsBanningHimself(playerNickname, userNickname);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void UserIsBanningHimself_WhenFalse_ReturnsFalse()
    {
        // Arrange
        const string playerNickname = "playerNickname";
        const string userNickname = "userNickname";

        // Act
        var result = PlayerExtensions.UserIsBanningHimself(playerNickname, userNickname);

        // Assert
        result.Should().BeFalse();
    }
}