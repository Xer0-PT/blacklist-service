namespace BlackList.Domain.Entities;

public class BlackListedPlayer : EntityBase
{
    public BlackListedPlayer()
    {}

    public BlackListedPlayer(Guid faceitId, string nickname,  DateTimeOffset createdAt)
    {
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public Guid FaceitId { get; set; }
    public string Nickname { get; set; } 
    public ICollection<User> Users { get; set; }
}
