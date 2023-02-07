using BlackList.Domain.Entities;
using FluentAssertions;

namespace BlackList.Domain.UnitTests.Entities;

public class PlayerTests
{
    private readonly DateTime _date = new (2023, 02, 06, 12, 12, 12);

    [Fact]
    public void PlayerConstructor_SetsProperties()
    {
        // Arrange
        const int userId = 1001;
        var faceitId = Guid.NewGuid();
        const string nickname = "test";

        // Act
        var player = new Player(userId, faceitId, nickname, _date);
        
        // Assert
        player.UserId.Should().Be(userId);
        player.FaceitId.Should().Be(faceitId);
        player.Nickname.Should().Be(nickname);
        player.CreatedAt.Should().Be(_date);
        player.Banned.Should().BeTrue();
    }
    
    [Fact]
    public void Player_BanMethod_SetsBannedToTrue()
    {
        // Arrange
        var player = new Player() { Banned = false };
        
        // Act
        player.Ban();
        
        // Assert
        player.Banned.Should().BeTrue();
    }
    
    [Fact]
    public void Player_UndoBanMethod_SetsBannedToFalse()
    {
        // Arrange
        var player = new Player();
        
        // Act
        player.UndoBan();
        
        // Assert
        player.Banned.Should().BeFalse();
    }
}