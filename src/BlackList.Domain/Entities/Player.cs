namespace BlackList.Domain.Entities;

public class Player : EntityBase
{
    public Player()
    { }

    public Player(long userId, Guid faceitId, string nickname, DateTimeOffset createdAt)
    {
        UserId = userId;
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public bool Banned { get; set; } = true;
    public long UserId { get; set; }
    
    public void UndoBan()
    {
        Banned = false;
    }

    public void Ban()
    {
        Banned = true;
    }
}
