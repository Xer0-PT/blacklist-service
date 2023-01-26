using BlackList.Domain.Entities;

namespace BlackList.Application.Extensions;

public static class PlayerExtensions
{
    public static bool PlayerIsAlreadyBanned(Player? player)
        => player is not null && player.Banned;

    public static bool IsReban(Player? player)
        => player is not null && !player.Banned;

    public static bool UserIsBanningHimself(string playerNickname, string userNickname)
        => userNickname.Equals(playerNickname, StringComparison.OrdinalIgnoreCase);
}