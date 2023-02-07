using BlackList.Domain.Entities;
using FluentAssertions;

namespace BlackList.Domain.UnitTests.Entities;

public class UserTests
{
    private readonly DateTime _date = new (2023, 02, 06, 12, 12, 12);

    [Fact]
    public void UserConstructor_SetsProperties()
    {
        // Arrange
        var faceitId = Guid.NewGuid();
        const string nickname = "test";

        // Act
        var user = new User(nickname, faceitId, _date);
        
        // Assert
        user.Nickname.Should().Be(nickname);
        user.FaceitId.Should().Be(faceitId);
        user.CreatedAt.Should().Be(_date);
    }
}