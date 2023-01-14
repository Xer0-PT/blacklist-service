namespace BlackList.Application.Services;

using BlackList.Application.Abstractions;
using System;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
