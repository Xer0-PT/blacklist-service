namespace BlackList.Application.Abstractions;

using Domain.Entities;
using System.Threading.Tasks;

public interface IPlayerRepository
{
    Task<Player> CreatePlayerAsync(User user, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<Player>> GetAllPlayersAsync(long userId, CancellationToken cancellationToken);
    Task<Player?> GetPlayerAsync(string playerNickname, long userId, CancellationToken cancellationToken);
    Task SaveChangesAsync(Player player, CancellationToken cancellationToken);
}
