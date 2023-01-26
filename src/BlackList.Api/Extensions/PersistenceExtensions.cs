using BlackList.Application.Abstractions;
using BlackList.Persistence.Data;
using BlackList.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace BlackList.Api.Extensions;

public static class PersistenceExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("BlackList");
        
        services.AddDbContext<BlackListServiceDbContext>(opt =>
        {
            opt.UseNpgsql(
                connectionString,
                options => options.EnableRetryOnFailure());
        });
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IBlackListedPlayerRepository, BlackListedPlayerRepository>();
    }
}