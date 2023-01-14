namespace BlackList.Application.Abstractions;

using BlackList.Application.Dtos;

public interface IBlackListedPlayerService
{
    Task<BlackListedPlayerDto?> CreateBlackListedPlayerAsync(string token, string nickname, CancellationToken cancellationToken);
    Task<IReadOnlyList<BlackListedPlayerDto>?> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken);
}
