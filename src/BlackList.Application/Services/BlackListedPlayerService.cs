namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;


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

        var player = await _repository.CreateBlackListedPlayerAsync(user, playerFaceItId, playerNickname, cancellationToken);

        return _mapper.Map<BlackListedPlayerDto>(player);
    }
    public async Task<IReadOnlyList<BlackListedPlayerDto>?> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userFaceitId, cancellationToken);

        var list = await _repository.GetAllBlackListedPlayersAsync(user.Id, cancellationToken);

        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }
    public async Task<BlackListedPlayerDto> UndoPlayer(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        return null;
    }

    private async Task<User> GetUserAsync(Guid userFaceitId, CancellationToken cancellationToken)
        => await _userRepository.GetUserAsync(userFaceitId, cancellationToken) ?? throw new ArgumentNullException(nameof(User), "This user does not exist!");

}
