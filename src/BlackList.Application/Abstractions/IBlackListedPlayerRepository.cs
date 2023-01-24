﻿namespace BlackList.Application.Abstractions;

using BlackList.Domain.Entities;
using System.Threading.Tasks;

public interface IBlackListedPlayerRepository
{
    Task<BlackListedPlayer> CreateBlackListedPlayerAsync(User user, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayer>?> GetAllBlackListedPlayersAsync(long userId, CancellationToken cancellationToken);
}
