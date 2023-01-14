namespace BlackList.Application.Abstractions;

using System;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
