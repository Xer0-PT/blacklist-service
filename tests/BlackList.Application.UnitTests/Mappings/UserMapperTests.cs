using AutoFixture;
using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Application.Mappings;
using BlackList.Domain.Entities;
using FluentAssertions;

namespace BlackList.Application.UnitTests.Mappings;

public class UserMapperTests
{
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly Fixture _fixture;

    public UserMapperTests()
    {
        _fixture = new Fixture();
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMapper>();
        });
    }

    [Fact]
    public void Map_UserToUserDto_KeepsProperties()
    {
        // Arrange
        var mapper = _mapperConfiguration.CreateMapper();
        var user = _fixture.Create<User>();
        
        // Act
        var dto = mapper.Map<UserDto>(user);
        
        // Assert
        dto.FaceitId.Should().Be(user.FaceitId);
    }
}