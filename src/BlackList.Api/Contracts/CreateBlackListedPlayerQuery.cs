namespace BlackList.Api.Contracts;

using System.ComponentModel.DataAnnotations;

public class CreateBlackListedPlayerQuery
{
    public CreateBlackListedPlayerQuery(string nickname, Guid userFaceitId)
    {
        Nickname = nickname;
        UserFaceitId = userFaceitId;

    }

    [Required]
    public string Nickname { get; set; }

    [Required]
    public Guid UserFaceitId { get; set;}
}
