namespace BlackList.Application.Abstractions;

using BlackList.Domain.Entities;
using System.Threading.Tasks;

public interface IBlackListRepository
{
    Task<BlackListedPlayer> CreateBlackListedPlayerAsync(string nickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayer>> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken);
}
