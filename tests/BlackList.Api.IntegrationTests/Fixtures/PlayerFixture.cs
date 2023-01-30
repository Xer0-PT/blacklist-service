using BlackList.Domain.Entities;

namespace BlackList.Api.IntegrationTests.Fixtures;

public static class PlayerFixture
{
    private static Player CreatePlayer(long userId, Guid? faceitId = null, string? nickname = null,
        DateTimeOffset? createdAt = null) =>
        new(userId, faceitId ?? Guid.NewGuid(), nickname ?? "teste", createdAt ?? DateTimeOffset.UtcNow);

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