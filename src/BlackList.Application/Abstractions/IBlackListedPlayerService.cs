namespace BlackList.Application.Abstractions;

using BlackList.Application.Dtos;

public interface IBlackListedPlayerService
{
    Task<BlackListedPlayerDto?> CreateBlackListedPlayerAsync(Guid userFaceitId,string playerNickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayerDto>?> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken);
    Task<BlackListedPlayerDto?> UndoPlayer(Guid useruserFaceitId, string playerNickname, CancellationToken cancellationToken); 
}
