namespace BlackList.Application.Services;

using Abstractions;
using AutoMapper;
using Dtos;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFaceitGateway _faceitGateway;

    public UserService(IUserRepository repository, IMapper mapper, IFaceitGateway faceitGateway)
    {
        _repository = repository;
        _mapper = mapper;
        _faceitGateway = faceitGateway;
    }

    public async Task<UserDto> CreateUserAsync(string nickname, CancellationToken cancellationToken)
    {
        var userFaceitId = await _faceitGateway.GetFaceitIdAsync(nickname, cancellationToken);

        var user = await _repository.GetUserAsync(userFaceitId, cancellationToken);

        if (user is not null)
        {
            throw new InvalidOperationException("This user already exists!");
        }

        user = await _repository.CreateUserAsync(nickname, userFaceitId, cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}
