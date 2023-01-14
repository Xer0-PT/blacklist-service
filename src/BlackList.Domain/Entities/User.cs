namespace BlackList.Domain.Entities;

public class User : EntityBase
{
    public User(string token, DateTimeOffset createdAt)
    {
        Token = token;
        BlackListedPlayers = new List<BlackListedPlayer>();
        CreatedAt = createdAt;
    }

    public string Token { get; set; } = string.Empty;
    public ICollection<BlackListedPlayer> BlackListedPlayers { get; set; }
}
