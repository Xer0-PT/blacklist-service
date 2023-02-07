using BlackList.Application.Services;

namespace BlackList.Application.UnitTests.Services;

public class DateTimeProviderTests
{
    private readonly DateTimeProvider _target;
    private readonly DateTime _date = new (2023, 02, 06, 12, 12, 12);

    public DateTimeProviderTests()
    {
        // _target = new DateTimeProvider(_date);
    }

    [Fact(Skip = "Solve DateTimeProvider dependency")]
    public void DateTimeProvider_GivesExpectedDateTime()
    {
        var expectedDate = new DateTime(2023, 02, 06, 12, 12, 12);
        var date = _target.UtcNow;
        
        Assert.Equal(date, expectedDate);
    }
}