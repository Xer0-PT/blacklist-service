namespace BlackList.Application.Abstractions;

using BlackList.Domain.Entities;
using System.Threading.Tasks;

public interface IBlackListedPlayerRepository
{
    Task<BlackListedPlayer> CreateBlackListedPlayerAsync(Guid userFaceitId, Guid playerFaceitId, string playerNickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayer>?> GetAllBlackListedPlayersAsync(Guid userFaceItId, CancellationToken cancellationToken);
}
