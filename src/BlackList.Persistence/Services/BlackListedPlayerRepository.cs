namespace BlackList.Persistence.Services;

using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<BlackListedPlayer> CreateBlackListedPlayerAsync(long userId, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        var player = await _context.BlackListedPlayer
            .FirstOrDefaultAsync(x => x.User.Id == userId && x.Nickname == playerNickname, cancellationToken);

        if (player is not null)
        {
            throw new InvalidOperationException("This user already has this player blacklisted!");
        }

        var blackListedPlayer = new BlackListedPlayer(playerFaceitId, playerNickname, _dateTimeProvider.UtcNow);

        _context.BlackListedPlayer.Add(blackListedPlayer);
        await _context.SaveChangesAsync(cancellationToken);

        return blackListedPlayer;
    }

    public async Task<IReadOnlyList<BlackListedPlayer>?> GetAllBlackListedPlayersAsync(long userId, CancellationToken cancellationToken)
        => await _context.BlackListedPlayer
        .Where(x => x.User.Id == userId)
        .Distinct()
        .OrderBy(x => x.Nickname)
        .ToListAsync(cancellationToken);
}
