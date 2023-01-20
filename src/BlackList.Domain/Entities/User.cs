namespace BlackList.Domain.Entities;

public class User : EntityBase
{
    public User(string nickname, Guid faceItId, DateTimeOffset createdAt)
    {
        FaceItId = faceItId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }

    public Guid FaceItId { get; set; }
    public string Nickname { get; set; }
    public ICollection<BlackListedPlayer> BlackListedPlayers { get; set; }
}
