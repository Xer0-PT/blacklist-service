namespace BlackList.Domain.Entities;

public class User
{
    public User(string token)
    {
        Token = token;
    }

    public long Id { get; set; }
    public string Token { get; set; }
}
