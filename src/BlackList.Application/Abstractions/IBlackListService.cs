namespace BlackList.Application.Abstractions;

using BlackList.Application.Dtos;

public interface IBlackListService
{
    Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(string nickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken);
}
