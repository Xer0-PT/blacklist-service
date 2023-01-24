namespace BlackList.Domain.Entities;

public class BlackListedPlayer : EntityBase
{
    public BlackListedPlayer()
    {}

    public BlackListedPlayer(User user, Guid faceitId, string nickname, DateTimeOffset createdAt)
    {
        User = user;
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public bool Banned { get; set; } = true;
    public User User { get; set; } = null!;
}
