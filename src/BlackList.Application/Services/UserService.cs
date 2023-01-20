namespace BlackList.Application.Services;

using AutoMapper;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using System;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUserAsync(string nickname, CancellationToken cancellationToken)
    {
        // TODO -> Get FaceitId with Refit
        Guid guid = Guid.NewGuid();
        var user = await _repository.CreateUserAsync(nickname, guid, cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
