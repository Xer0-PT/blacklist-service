using BlackList.Application.Abstractions;

namespace BlackList.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    
    // private readonly DateTime? _dateTime;
    // public DateTimeProvider(DateTime fixedDateTime)
    //     => _dateTime = fixedDateTime;
    // public DateTimeOffset UtcNow => _dateTime ?? DateTimeOffset.UtcNow;
}
