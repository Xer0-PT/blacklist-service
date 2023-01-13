namespace BlackList.Persistence.Services;

using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using System.Threading;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly BlackListServiceDbContext _context;

    public UserRepository(BlackListServiceDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(string token, CancellationToken cancellationToken)
    {
        var user = new User(token);

        _context.User.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}
