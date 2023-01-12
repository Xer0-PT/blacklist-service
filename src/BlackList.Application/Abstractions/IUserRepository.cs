namespace BlackList.Application.Abstractions;

using BlackList.Domain.Entities;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> CreateUserAsync(string token, CancellationToken cancellationToken);
}
