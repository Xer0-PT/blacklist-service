namespace BlackList.Domain.Entities;

public class BlackListedPlayer : EntityBase
{
    public BlackListedPlayer()
    {}

    public BlackListedPlayer(long userId, string nickname, DateTimeOffset createdAt)
    {
        UserId = userId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public long UserId { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}
