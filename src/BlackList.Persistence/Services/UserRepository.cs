namespace BlackList.Persistence.Services;

using BlackList.Application.Abstractions;
using BlackList.Domain.Entities;
using BlackList.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly BlackListServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserRepository(BlackListServiceDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<User> CreateUserAsync(string nickname, Guid userFaceitId, CancellationToken cancellationToken)
    {
        var user = new User(nickname, userFaceitId, _dateTimeProvider.UtcNow);

        _context.User.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> GetUserAsync(Guid faceitId, CancellationToken cancellationToken)
        => await _context.User
        .FirstOrDefaultAsync(x => x.FaceitId == faceitId, cancellationToken);
}
