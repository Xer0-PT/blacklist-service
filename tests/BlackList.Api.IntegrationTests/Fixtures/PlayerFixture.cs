using BlackList.Domain.Entities;

namespace BlackList.Api.IntegrationTests.Fixtures;

public static class PlayerFixture
{
    public static Player CreatePlayer(long userId, Guid? faceitId = null, string? nickname = null,
        DateTimeOffset? createdAt = null, bool? banned = null) =>
        new(userId, faceitId ?? Guid.NewGuid(), nickname ?? "player", createdAt ?? DateTimeOffset.UtcNow)
            { Banned = banned ?? true };

    public static Player[] CreatePlayerRange(int size, long userId)
    {
        var list = new List<Player>();
        
        for (var i = 1; i <= size; i++)
        {
            list.Add(CreatePlayer(userId: userId, nickname: $"teste{i:0000}"));
        }

        return list.ToArray();
    }
}