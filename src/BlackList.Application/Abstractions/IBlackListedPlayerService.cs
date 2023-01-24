namespace BlackList.Application.Abstractions;

using Dtos;

public interface IBlackListedPlayerService
{
    Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(Guid userFaceitId,string playerNickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken);
}
