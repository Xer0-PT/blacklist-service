using BlackList.Domain.Entities;

namespace BlackList.Api.IntegrationTests.Fixtures;

public static class UserFixture
{
    public static User CreateUser(string? nickname = null, Guid? faceitId = null, DateTimeOffset? createdAt = null) =>
        new(nickname ?? "teste", faceitId ?? Guid.NewGuid(), createdAt ?? DateTimeOffset.UtcNow);
}