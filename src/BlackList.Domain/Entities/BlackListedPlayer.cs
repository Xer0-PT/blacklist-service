namespace BlackList.Domain.Entities;

public class BlackListedPlayer
{
    public BlackListedPlayer(string nickname, ICollection<long> userId)
    {
        Nickname = nickname;
        UserId = userId;
    }

    public long Id { get; set; }
    public string Nickname { get; set; }
    public ICollection<long> UserId { get; set; }
}