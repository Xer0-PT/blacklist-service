using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlackList.Persistence.Services;

public class BlackListedPlayerRepository : IBlackListedPlayerRepository
{
    private readonly BlackListServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BlackListedPlayerRepository(BlackListServiceDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<BlackListedPlayer> CreateBlackListedPlayerAsync(User user, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        var player = await _context.BlackListedPlayer
            .FirstOrDefaultAsync(x => x.User.Id == user.Id && x.Nickname == playerNickname, cancellationToken);

        if (player is not null)
        {
            throw new InvalidOperationException("This user already has this player blacklisted!");
        }

        var blackListedPlayer = new BlackListedPlayer(user, playerFaceitId, playerNickname, _dateTimeProvider.UtcNow);

        _context.BlackListedPlayer.Add(blackListedPlayer);
        await _context.SaveChangesAsync(cancellationToken);

        return blackListedPlayer;
    }

    public async Task<IReadOnlyList<BlackListedPlayer>?> GetAllBlackListedPlayersAsync(long userId, CancellationToken cancellationToken)
        => await _context.BlackListedPlayer
        .Where(x => x.User.Id == userId && x.Banned == true)
        .Distinct()
        .OrderBy(x => x.Nickname)
        .ToListAsync(cancellationToken);

    public async Task<BlackListedPlayer?> GetBlackListedPlayerAsync(string playerNickname, long userId, CancellationToken cancellationToken)
        => await _context.BlackListedPlayer
            .Where(x => EF.Functions.ILike(x.Nickname, playerNickname) && x.User.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task SaveChangesAsync(BlackListedPlayer player, CancellationToken cancellationToken)
    {
        _context.BlackListedPlayer.Update(player);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
