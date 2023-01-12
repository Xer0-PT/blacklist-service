namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class BlackListService : IBlackListService
{
    private readonly IBlackListRepository _repository;
    private readonly IMapper _mapper;

    public BlackListService(IBlackListRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BlackListedPlayerDto> CreateBlackListedPlayerAsync(string nickname, CancellationToken cancellationToken)
    {
        var player = await _repository.CreateBlackListedPlayerAsync(nickname, cancellationToken);
        
        return _mapper.Map<BlackListedPlayerDto>(player);
    }

    public async Task<IReadOnlyList<BlackListedPlayerDto>> GetAllBlackListedPlayersAsync(string token, CancellationToken cancellationToken)
    {
        var list = await _repository.GetAllBlackListedPlayersAsync(token, cancellationToken);
        return _mapper.Map<IReadOnlyList<BlackListedPlayerDto>>(list);
    }
}
