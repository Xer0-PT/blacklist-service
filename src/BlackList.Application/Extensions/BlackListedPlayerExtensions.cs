using BlackList.Domain.Entities;

namespace BlackList.Application.Extensions;

public static class BlackListedPlayerExtensions
{
    public static bool PlayerIsAlreadyBanned(BlackListedPlayer? player)
        => player is not null && player.Banned;

    public static bool IsReban(BlackListedPlayer? player)
        => player is not null && !player.Banned;

    public static bool UserIsBanningHimself(string playerNickname, string userNickname)
        => userNickname.Equals(playerNickname, StringComparison.OrdinalIgnoreCase);
}