namespace BlackList.Persistence.Services;

using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class BlackListRepository : IBlackListRepository
{
    public Task<BlackListedPlayer> CreateBlackListedPlayerAsync(string nickname, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<BlackListedPlayer>> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
