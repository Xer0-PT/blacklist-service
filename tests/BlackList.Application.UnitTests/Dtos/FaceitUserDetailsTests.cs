using BlackList.Application.Dtos;
using FluentAssertions;

namespace BlackList.Application.UnitTests.Dtos;

public class FaceitUserDetailsTests
{
    [Fact]
    public void Setting_NewFaceitUserDetails_CreatesWithExpectedProperties()
    {
        // Arrange
        var newGuid = Guid.NewGuid();
        
        // Act
        var faceitUserDetails = new FaceitUserDetails { PlayerId = newGuid };
        
        // Assert
        faceitUserDetails.PlayerId.Should().Be(newGuid);
    }
}