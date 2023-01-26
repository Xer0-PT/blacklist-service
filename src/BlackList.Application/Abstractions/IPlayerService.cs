namespace BlackList.Application.Abstractions;

using Dtos;

public interface IPlayerService
{
    Task<IReadOnlyList<PlayerDto>> GetAllPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken);
    Task<PlayerDto> CreatePlayerAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken);
    Task<PlayerDto> UndoPlayerBanAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken);
}
