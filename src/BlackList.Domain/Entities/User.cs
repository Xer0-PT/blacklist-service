namespace BlackList.Domain.Entities;

public class User : EntityBase
{
    public User(string nickname, Guid faceitId, DateTimeOffset createdAt)
    {
        FaceitId = faceitId;
        Nickname = nickname;
        CreatedAt = createdAt;
    }
}
