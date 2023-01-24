namespace BlackList.Api.Contracts;

using System.ComponentModel.DataAnnotations;

public class CreateBlackListedPlayerQuery
{
    public CreateBlackListedPlayerQuery(string playerNickname, Guid userFaceitId)
    {
        PlayerNickname = playerNickname;
        UserFaceitId = userFaceitId;

    }

    [Required]
    public string PlayerNickname { get; set; }

    [Required]
    public Guid UserFaceitId { get; set;}
}
