namespace BlackList.Domain.Entities;

public class BlackListedPlayer
{
    public BlackListedPlayer()
    {
    }

    public BlackListedPlayer(string nickname, long userId, User user)
    {
        Nickname = nickname;
        UserId = userId;
        User = user;
    }

    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string Nickname { get; set; }
}