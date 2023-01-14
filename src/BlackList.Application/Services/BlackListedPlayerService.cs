namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using System.Collections.Generic;
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

    public async Task<BlackListedPlayerDto?> CreateBlackListedPlayerAsync(string token, string nickname, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserIdAsync(token, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var player = await _repository.CreateBlackListedPlayerAsync(user.Id, nickname, cancellationToken);
        
        return _mapper.Map<BlackListedPlayerDto>(player);
    }

    public async Task<IReadOnlyList<BlackListedPlayerDto>?> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserIdAsync(token, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var list = await _repository.GetAllBlackListedPlayersAsync(user.Id, cancellationToken);

        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }
}
