using BlackList.Application.Abstractions;

namespace BlackList.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
