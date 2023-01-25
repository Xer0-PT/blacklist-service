namespace BlackList.Application.Abstractions;

using Dtos;

public interface IBlackListedPlayerService
{
    Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken);
    Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken);
    Task<BlackListedPlayerDto> UndoPlayerBanAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken);
}
