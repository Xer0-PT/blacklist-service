namespace BlackList.Domain.Entities;

public class Player : EntityBase
{
    public Player()
    { }

    public Player(User user, Guid faceitId, string nickname, DateTimeOffset createdAt)
    {
        User = user;
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public bool Banned { get; set; } = true;
    public User User { get; set; } = null!;

    public void UndoBan()
    {
        Banned = false;
    }

    public void Ban()
    {
        Banned = true;
    }
}
