using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using BlackList.Application.Extensions;
using BlackList.Domain.Entities;

namespace BlackList.Application.Services;

public class BlackListedPlayerService : IBlackListedPlayerService
{
    private readonly IUserRepository _userRepository;
    private readonly IBlackListedPlayerRepository _playerRepository;
    private readonly IMapper _mapper;
    private readonly IFaceitGateway _faceitGateway;

    public BlackListedPlayerService(IBlackListedPlayerRepository repository, IMapper mapper,
        IUserRepository userRepository, IFaceitGateway faceitGateway)
    {
        _playerRepository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _faceitGateway = faceitGateway;
    }

    public async Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(Guid userFaceitId, string playerNickname,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        if (BlackListedPlayerExtensions.UserIsBanningHimself(playerNickname, user.Nickname))
        {
            throw new InvalidOperationException("You cannot ban yourself!");
        }

        var player = await _playerRepository.GetBlackListedPlayerAsync(playerNickname, user.Id, cancellationToken);

        if (BlackListedPlayerExtensions.IsReban(player))
        {
            return await RebanPlayerAndSaveChangesAsync(player!, cancellationToken);
        }

        if (BlackListedPlayerExtensions.PlayerIsAlreadyBanned(player))
        {
            throw new InvalidOperationException("This user already has this player blacklisted!");
        }
        
        var playerFaceItId = await _faceitGateway.GetFaceitIdAsync(playerNickname, cancellationToken);

        player = await _playerRepository.CreateBlackListedPlayerAsync(user, playerFaceItId, playerNickname,
            cancellationToken);

        return _mapper.Map<BlackListedPlayerDto>(player);
    }

    public async Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(Guid userFaceitId,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        var list = await _playerRepository.GetAllBlackListedPlayersAsync(user.Id, cancellationToken);

        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }

    public async Task<BlackListedPlayerDto> UndoPlayerBanAsync(Guid userFaceitId, string playerNickname,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        var player = await TryGetPlayerAsync(playerNickname, user.Id, cancellationToken);

        player.UndoBan();

        await _playerRepository.SaveChangesAsync(player, cancellationToken);

        return _mapper.Map<BlackListedPlayerDto>(player);
    }
    
    private async Task<BlackListedPlayerDto> RebanPlayerAndSaveChangesAsync(BlackListedPlayer player,
        CancellationToken cancellationToken)
    {
        player.Ban();
        await _playerRepository.SaveChangesAsync(player, cancellationToken);
        return _mapper.Map<BlackListedPlayerDto>(player);
    }

    private async Task<BlackListedPlayer> TryGetPlayerAsync(string playerNickname, long userId,
        CancellationToken cancellationToken)
        => await _playerRepository.GetBlackListedPlayerAsync(playerNickname, userId, cancellationToken) ??
           throw new ArgumentNullException(playerNickname, "This player does not exist!");

    private async Task<User> TryGetUserAsync(Guid userFaceitId, CancellationToken cancellationToken)
        => await _userRepository.GetUserAsync(userFaceitId, cancellationToken) ??
           throw new ArgumentNullException(nameof(User), "This user does not exist!");
}