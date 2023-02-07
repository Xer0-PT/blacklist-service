using System.Diagnostics.CodeAnalysis;
using AspNetCoreRateLimit;

namespace BlackList.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class RateLimitingExtensions
{
    public static void AddRateLimitingServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var ipRateLimiting = configuration.GetSection("IpRateLimiting");
        var ipRateLimitPolicies = configuration.GetSection("IpRateLimitPolicies");
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(ipRateLimiting);
        services.Configure<IpRateLimitPolicies>(ipRateLimitPolicies);
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}