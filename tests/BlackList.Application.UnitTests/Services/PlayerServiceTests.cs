using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using BlackList.Application.Services;
using BlackList.Domain.Entities;
using FluentAssertions;
using Moq;

namespace BlackList.Application.UnitTests.Services;

public class PlayerServiceTests
{
    private readonly Mock<IPlayerRepository> _playerRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IFaceitGateway> _gateway;

    private readonly PlayerService _target;

    public PlayerServiceTests()
    {
        _playerRepository = new Mock<IPlayerRepository>();
        _userRepository = new Mock<IUserRepository>();
        _mapper = new Mock<IMapper>();
        _gateway = new Mock<IFaceitGateway>();

        _target = new PlayerService(_playerRepository.Object, _mapper.Object, _userRepository.Object, _gateway.Object);
    }

    [Fact]
    public async Task CreatePlayer_WhenUserIsNull_ThrowsKeyNotFoundException()
    {
        // Arrange
        _userRepository
            .Setup(x => x.GetUserAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((User)null!)
            .Verifiable();

        // Act
        // Assert
        await _target.Invoking(x => x.CreatePlayerAsync(It.IsAny<Guid>(), "", default))
            .Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("This user does not exist!");
        _userRepository.Verify();
    }

    [Fact]
    public async Task CreatePlayer_WhenUserIsBanningHimself_ThrowsInvalidOperationException()
    {
        // Arrange
        const string nickname = "nickname";

        _userRepository
            .Setup(x => x.GetUserAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(new User(nickname, It.IsAny<Guid>(), default))
            .Verifiable();

        // Act
        // Assert
        await _target.Invoking(x => x.CreatePlayerAsync(It.IsAny<Guid>(), nickname, default))
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("You cannot ban yourself!");
        _userRepository.Verify();
    }

    [Fact]
    public async Task CreatePlayer_WhenUserIsRebanning_RebansPlayer()
    {
        // Arrange
        var player = new Player { Banned = false };
        
        _userRepository
            .Setup(x => x.GetUserAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(new User("nickname", It.IsAny<Guid>(), default))
            .Verifiable();

        _playerRepository
            .Setup(x => x.GetPlayerAsync("", default, default))
            .ReturnsAsync(player)
            .Verifiable();

        _playerRepository
            .Setup(x => x.SaveChangesAsync(player, default))
            .Verifiable();

        // Act
        await _target.CreatePlayerAsync(It.IsAny<Guid>(), "", default);
        
        // Assert
        player.Banned.Should().BeTrue();
        _userRepository.Verify();
        _playerRepository.Verify();
    }
    
    [Fact]
    public async Task CreatePlayer_WhenPlayerIsBanned_ThrowsInvalidOperationException()
    {
        // Arrange
        var user = new User("nickname", It.IsAny<Guid>(), default);
        var player = new Player { Banned = true };

        _userRepository
            .Setup(x => x.GetUserAsync(user.FaceitId, default))
            .ReturnsAsync(user)
            .Verifiable();

        _playerRepository
            .Setup(x => x.GetPlayerAsync(player.Nickname, user.Id, default))
            .ReturnsAsync(player)
            .Verifiable();

        // Act
        // Assert
        await _target.Invoking(x => x.CreatePlayerAsync(It.IsAny<Guid>(), player.Nickname, default))
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("This user already has this player blacklisted!");
        _userRepository.Verify();
        _playerRepository.Verify();
    }
    
    [Fact]
    public async Task CreatePlayer_WithCorrectData_CreatesPlayer()
    {
        // Arrange
        const string playerNickname = "player";
        var guid = Guid.NewGuid();
        var user = new User("nickname", It.IsAny<Guid>(), default);

        _userRepository
            .Setup(x => x.GetUserAsync(user.FaceitId, default))
            .ReturnsAsync(user)
            .Verifiable();

        _playerRepository
            .Setup(x => x.GetPlayerAsync(playerNickname, user.Id, default))
            .ReturnsAsync((Player)null!)
            .Verifiable();
        
        _gateway
            .Setup(x => x.GetFaceitIdAsync(playerNickname, default))
            .ReturnsAsync(guid)
            .Verifiable();

        // Act
        await _target.CreatePlayerAsync(user.FaceitId, playerNickname, default);
        
        // Assert
        _userRepository.Verify();
        _playerRepository.Verify();
        _gateway.Verify();
    }

    [Fact]
    public async Task GetAllPlayers_WithNonExistingUser_ThrowsKeyNotFoundException()
    {
        // Arrange
        _userRepository
            .Setup(x => x.GetUserAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((User)null!)
            .Verifiable();
        
        // Act
        // Assert
        await _target.Invoking(x => x.GetAllPlayersAsync(It.IsAny<Guid>(), default))
            .Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("This user does not exist!");
        _userRepository.Verify();
    }
    
    [Fact]
    public async Task GetAllPlayers_WithExistingUser_ReturnsPlayerList()
    {
        // Arrange
        var user = new User("nickname", It.IsAny<Guid>(), default);
        
        _userRepository
            .Setup(x => x.GetUserAsync(user.FaceitId, default))
            .ReturnsAsync(user)
            .Verifiable();
        
        _playerRepository
            .Setup(x => x.GetAllPlayersAsync(user.Id, default))
            .ReturnsAsync(It.IsAny<IReadOnlyList<Player>>())
            .Verifiable();
        
        // Act
        await _target.GetAllPlayersAsync(user.FaceitId, default);
        
        // Assert
        _userRepository.Verify();
        _playerRepository.Verify();
    }
    
    [Fact]
    public async Task UndoPlayerBan_WithNonExistingUser_ThrowsKeyNotFoundException()
    {
        // Arrange
        _userRepository
            .Setup(x => x.GetUserAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((User)null!)
            .Verifiable();
        
        // Act
        // Assert
        await _target.Invoking(x => x.UndoPlayerBanAsync(It.IsAny<Guid>(), "", default))
            .Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("This user does not exist!");
        _userRepository.Verify();
    }
    
    [Fact]
    public async Task UndoPlayerBan_WithNonExistingPlayer_ThrowsKeyNotFoundException()
    {
        // Arrange
        const string playerNickname = "player";
        var user = new User("nickname", It.IsAny<Guid>(), default);
        
        _userRepository
            .Setup(x => x.GetUserAsync(user.FaceitId, default))
            .ReturnsAsync(user)
            .Verifiable();
        
        _playerRepository
            .Setup(x => x.GetPlayerAsync(playerNickname, It.IsAny<long>(), default))
            .ReturnsAsync((Player)null!)
            .Verifiable();
        
        // Act
        // Assert
        await _target.Invoking(x => x.UndoPlayerBanAsync(It.IsAny<Guid>(), playerNickname, default))
            .Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"The player {playerNickname} does not exist!");
        _userRepository.Verify();
        _playerRepository.Verify();
    }
    
    [Fact]
    public async Task UndoPlayerBan_WithCorrectData_RebansPlayer()
    {
        // Arrange
        var player = new Player();
        var user = new User("nickname", It.IsAny<Guid>(), default);
        
        _userRepository
            .Setup(x => x.GetUserAsync(user.FaceitId, default))
            .ReturnsAsync(user)
            .Verifiable();
        
        _playerRepository
            .Setup(x => x.GetPlayerAsync(player.Nickname, It.IsAny<long>(), default))
            .ReturnsAsync(player)
            .Verifiable();
        
        _playerRepository
            .Setup(x => x.SaveChangesAsync(player, default))
            .Verifiable();
        
        // Act
        await _target.UndoPlayerBanAsync(user.FaceitId, player.Nickname, default);
        
        // Assert
        player.Banned.Should().BeFalse();
        _userRepository.Verify();
        _playerRepository.Verify();
    }
}