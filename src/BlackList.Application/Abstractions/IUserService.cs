namespace BlackList.Application.Abstractions;

using BlackList.Application.Dtos;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(string nickname, CancellationToken cancellationToken);
}
