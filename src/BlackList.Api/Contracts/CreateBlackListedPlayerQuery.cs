namespace BlackList.Api.Contracts;

using System.ComponentModel.DataAnnotations;

public class CreateBlackListedPlayerQuery
{
    public CreateBlackListedPlayerQuery(string nickname, string token)
    {
        Nickname = nickname;
        Token = token;
    }

    [Required]
    public string Nickname { get; set; }
    
    [Required]
    public string Token { get; set; }
}
