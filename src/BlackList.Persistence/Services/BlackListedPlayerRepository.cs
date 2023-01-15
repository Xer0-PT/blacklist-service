namespace BlackList.Persistence.Services;

using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class BlackListedPlayerRepository : IBlackListedPlayerRepository
{
    private readonly BlackListServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BlackListedPlayerRepository(BlackListServiceDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<BlackListedPlayer> CreateBlackListedPlayerAsync(long userId, string nickname, CancellationToken cancellationToken)
    {
        var player = await _context.BlackListedPlayer.FirstOrDefaultAsync(x => x.UserId == userId && x.Nickname == nickname, cancellationToken);

        if (player is not null)
        {
            throw new InvalidOperationException();
        }

        var blackListedPlayer = new BlackListedPlayer(userId, nickname, _dateTimeProvider.UtcNow);

        _context.BlackListedPlayer.Add(blackListedPlayer);
        await _context.SaveChangesAsync(cancellationToken);

        return blackListedPlayer;
    }

    public async Task<IReadOnlyList<BlackListedPlayer>?> GetAllBlackListedPlayersAsync(long userId, CancellationToken cancellationToken)
        => await _context.BlackListedPlayer
        .Where(x => x.UserId == userId)
        .Distinct()
        .OrderBy(x => x.Nickname)
        .ToListAsync(cancellationToken);
}
