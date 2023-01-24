namespace BlackList.Application.Abstractions;

using BlackList.Domain.Entities;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> CreateUserAsync(string nickname,Guid userFaceitId, CancellationToken cancellationToken);
    Task<User?> GetUserAsync(Guid faceitId, CancellationToken cancellationToken);
}
