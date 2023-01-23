namespace BlackList.Domain.Entities;

public class BlackListedPlayer : EntityBase
{
    public BlackListedPlayer()
    {}

    public BlackListedPlayer(Guid faceitId, string nickname, DateTimeOffset createdAt)
    {
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public bool Banned { get; set; } = true;
    public User User { get; set; }
}
