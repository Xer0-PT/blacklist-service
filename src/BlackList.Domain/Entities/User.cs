namespace BlackList.Domain.Entities;

public class User
{
    public User(string token)
    {
        Token = token;
        BlackListedPlayers = new List<BlackListedPlayer>();
    }

    public long Id { get; set; }
    public string Token { get; set; }
    public ICollection<BlackListedPlayer> BlackListedPlayers { get; set; }
}
