using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Services;
using BlackList.Domain.Entities;
using Moq;

namespace BlackList.Application.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IFaceitGateway> _gateway;

    private readonly UserService _target;

    public UserServiceTests()
    {
        _repository = new Mock<IUserRepository>();
        _mapper = new Mock<IMapper>();
        _gateway = new Mock<IFaceitGateway>();

        _target = new UserService(_repository.Object, _mapper.Object, _gateway.Object);
    }

    [Fact]
    public async Task CreateUser_WhenUserIsNotNull_ReturnsExistingUser()
    {
        // Arrange
        const string nickname = "test";
        var newGuid = Guid.NewGuid();
        
        _gateway
            .Setup(x => x.GetFaceitIdAsync(nickname, default))
            .ReturnsAsync(newGuid)
            .Verifiable();
        
        _repository
            .Setup(x => x.GetUserAsync(newGuid, default))
            .ReturnsAsync(new User(nickname, newGuid, DateTimeOffset.Now))
            .Verifiable();

        // Act
        await _target.CreateUserAsync(nickname, default);

        // Assert
        _gateway.Verify();
        _repository.Verify();
        _mapper.Verify();
    }
    
    [Fact]
    public async Task CreateUser_WhenUserIsNull_CreatesNewUser()
    {
        // Arrange
        const string nickname = "test";
        var newGuid = Guid.NewGuid();
        
        _gateway
            .Setup(x => x.GetFaceitIdAsync(nickname, default))
            .ReturnsAsync(newGuid)
            .Verifiable();
        
        _repository
            .Setup(x => x.GetUserAsync(newGuid, default))
            .Verifiable();
        
        
        _repository
            .Setup(x => x.CreateUserAsync(nickname, newGuid, default))
            .ReturnsAsync(new User(nickname, newGuid, DateTimeOffset.Now))
            .Verifiable();

        // Act
        await _target.CreateUserAsync(nickname, default);

        // Assert
        _gateway.Verify();
        _repository.Verify();
        _mapper.Verify();
    }
}