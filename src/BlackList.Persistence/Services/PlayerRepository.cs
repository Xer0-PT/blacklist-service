using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlackList.Persistence.Services;

public class PlayerRepository : IPlayerRepository
{
    private readonly BlackListServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PlayerRepository(BlackListServiceDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Player> CreatePlayerAsync(User user, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        var player = new Player(user, playerFaceitId, playerNickname, _dateTimeProvider.UtcNow);

        _context.Player.Add(player);
        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }

    public async Task<IReadOnlyList<Player>> GetAllPlayersAsync(long userId, CancellationToken cancellationToken)
        => await _context.Player
            .Where(x => x.User.Id == userId && x.Banned == true)
            .Distinct()
            .OrderBy(x => x.Nickname)
            .ToListAsync(cancellationToken);

    public async Task<Player?> GetPlayerAsync(string playerNickname, long userId, CancellationToken cancellationToken)
        => await _context.Player
            .Where(x => EF.Functions.ILike(x.Nickname, playerNickname) && x.User.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task SaveChangesAsync(Player player, CancellationToken cancellationToken)
    {
        _context.Player.Update(player);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
