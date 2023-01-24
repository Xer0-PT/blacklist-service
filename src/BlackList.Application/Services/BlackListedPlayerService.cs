using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;

namespace BlackList.Application.Services;

public class BlackListedPlayerService : IBlackListedPlayerService
{
    private readonly IUserRepository _userRepository;
    private readonly IBlackListedPlayerRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFaceitGateway _faceitGateway;

    public BlackListedPlayerService(IBlackListedPlayerRepository repository, IMapper mapper, IUserRepository userRepository, IFaceitGateway faceitGateway)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _faceitGateway = faceitGateway;
    }

    public async Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userFaceitId, cancellationToken);

        var playerFaceItId = await _faceitGateway.GetFaceitIdAsync(playerNickname, cancellationToken);
        
        CheckIfUserIsBanningHimself(playerNickname, user, playerFaceItId);

        var player = await _repository.CreateBlackListedPlayerAsync(user, playerFaceItId, playerNickname, cancellationToken);

        return _mapper.Map<BlackListedPlayerDto>(player);
    }

    public async Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userFaceitId, cancellationToken);

        var list = await _repository.GetAllBlackListedPlayersAsync(user.Id, cancellationToken);

        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }
    
    private static void CheckIfUserIsBanningHimself(string playerNickname, EntityBase user, Guid playerFaceItId)
    {
        if (user.Nickname.Equals(playerNickname, StringComparison.OrdinalIgnoreCase) || user.FaceitId == playerFaceItId)
        {
            throw new InvalidOperationException("You cannot ban yourself!");
        }
    }

    private async Task<User> GetUserAsync(Guid userFaceitId, CancellationToken cancellationToken)
        => await _userRepository.GetUserAsync(userFaceitId, cancellationToken) ?? throw new ArgumentNullException(nameof(User), "This user does not exist!");
}
