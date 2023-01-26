using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using BlackList.Application.Extensions;
using BlackList.Domain.Entities;

namespace BlackList.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IUserRepository _userRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;
    private readonly IFaceitGateway _faceitGateway;

    public PlayerService(IPlayerRepository repository, IMapper mapper,
        IUserRepository userRepository, IFaceitGateway faceitGateway)
    {
        _playerRepository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _faceitGateway = faceitGateway;
    }

    public async Task<PlayerDto> CreatePlayerAsync(Guid userFaceitId, string playerNickname,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        if (PlayerExtensions.UserIsBanningHimself(playerNickname, user.Nickname))
        {
            throw new InvalidOperationException("You cannot ban yourself!");
        }

        var player = await _playerRepository.GetPlayerAsync(playerNickname, user.Id, cancellationToken);

        if (PlayerExtensions.IsReban(player))
        {
            return await RebanPlayerAndSaveChangesAsync(player!, cancellationToken);
        }

        if (PlayerExtensions.PlayerIsAlreadyBanned(player))
        {
            throw new InvalidOperationException("This user already has this player blacklisted!");
        }
        
        var playerFaceItId = await _faceitGateway.GetFaceitIdAsync(playerNickname, cancellationToken);

        player = await _playerRepository.CreatePlayerAsync(user, playerFaceItId, playerNickname,
            cancellationToken);

        return _mapper.Map<PlayerDto>(player);
    }

    public async Task<IReadOnlyList<PlayerDto>> GetAllPlayersAsync(Guid userFaceitId,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        var list = await _playerRepository.GetAllPlayersAsync(user.Id, cancellationToken);

        return _mapper.Map<IReadOnlyList<PlayerDto>>(list);
    }

    public async Task<PlayerDto> UndoPlayerBanAsync(Guid userFaceitId, string playerNickname,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserAsync(userFaceitId, cancellationToken);

        var player = await TryGetPlayerAsync(playerNickname, user.Id, cancellationToken);

        player.UndoBan();

        await _playerRepository.SaveChangesAsync(player, cancellationToken);

        return _mapper.Map<PlayerDto>(player);
    }
    
    private async Task<PlayerDto> RebanPlayerAndSaveChangesAsync(Player player,
        CancellationToken cancellationToken)
    {
        player.Ban();
        await _playerRepository.SaveChangesAsync(player, cancellationToken);
        return _mapper.Map<PlayerDto>(player);
    }

    private async Task<Player> TryGetPlayerAsync(string playerNickname, long userId,
        CancellationToken cancellationToken)
        => await _playerRepository.GetPlayerAsync(playerNickname, userId, cancellationToken) ??
           throw new ArgumentNullException(playerNickname, "This player does not exist!");

    private async Task<User> TryGetUserAsync(Guid userFaceitId, CancellationToken cancellationToken)
        => await _userRepository.GetUserAsync(userFaceitId, cancellationToken) ??
           throw new ArgumentNullException(nameof(User), "This user does not exist!");
}