namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;


public class BlackListedPlayerService : IBlackListedPlayerService
{
    private readonly IUserRepository _userRepository;
    private readonly IBlackListedPlayerRepository _repository;
    private readonly IMapper _mapper;

    public BlackListedPlayerService(IBlackListedPlayerRepository repository, IMapper mapper, IUserRepository userRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<BlackListedPlayerDto?> CreateBlackListedPlayerAsync(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        //TODO -> Refactor GetUserIdAsync 
        //var user = await _userRepository.GetUserIdAsync(userFaceItId, cancellationToken);

        //if (user is null)
        //{
        //    return null;
        //}

        //TODO -> Get playerFaceitId

        Guid playerFaceItId = default;

        var player = await _repository.CreateBlackListedPlayerAsync(userFaceitId, playerFaceItId, playerNickname, cancellationToken);

        return _mapper.Map<BlackListedPlayerDto>(player);
    }
    public async Task<IReadOnlyList<BlackListedPlayerDto>?> GetAllBlackListedPlayersAsync(Guid userFaceitId, CancellationToken cancellationToken)
    {
        //TODO -> refactor GetUserIdAsync
        //var user = await _userRepository.GetUserIdAsync(nickName, cancellationToken);

        //if (user is null)
        //{
        //    return null;
        //}

        var list = await _repository.GetAllBlackListedPlayersAsync(userFaceitId, cancellationToken);

        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }
    public async Task<BlackListedPlayerDto> UndoPlayer(Guid userFaceitId, string playerNickname, CancellationToken cancellationToken)
    {
        return null;
    }
}
