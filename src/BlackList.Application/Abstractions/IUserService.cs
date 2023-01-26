namespace BlackList.Application.Abstractions;

using Dtos;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(string nickname, CancellationToken cancellationToken);
}
