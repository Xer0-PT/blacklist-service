using BlackList.Application.Abstractions;
using FluentAssertions;
using Moq;

namespace BlackList.Application.UnitTests.Services;

public class DateTimeProviderTests
{
    private readonly Mock<IDateTimeProvider> _mock;

    public DateTimeProviderTests()
    {
        _mock = new Mock<IDateTimeProvider>();
    }

    [Fact]
    public void DateTimeProvider_GivesExpectedDateTime()
    {
        // Arrange
        var expectedDate = DateTimeOffset.UtcNow;
        _mock
            .Setup(x => x.UtcNow)
            .Returns(expectedDate);

        // Act
        var date = _mock.Object.UtcNow;
        
        // Assert
        date.Should().Be(expectedDate);
    }
}