using AutoFixture;
using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Application.Mappings;
using BlackList.Domain.Entities;
using FluentAssertions;

namespace BlackList.Application.UnitTests.Mappings;

public class PlayerMapperTests
{
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly Fixture _fixture;

    public PlayerMapperTests()
    {
        _fixture = new Fixture();
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PlayerMapper>();
        });
    }

    [Fact]
    public void Map_PlayerToPlayerDto_KeepsProperties()
    {
        // Arrange
        var mapper = _mapperConfiguration.CreateMapper();
        var player = _fixture.Create<Player>();
        
        // Act
        var dto = mapper.Map<PlayerDto>(player);
        
        // Assert
        dto.Nickname.Should().Be(player.Nickname);
    }
}