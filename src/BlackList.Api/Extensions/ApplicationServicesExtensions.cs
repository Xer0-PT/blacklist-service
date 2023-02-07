using System.Diagnostics.CodeAnalysis;
using BlackList.Application.Abstractions;
using BlackList.Application.Services;
using Refit;

namespace BlackList.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var faceitUri = configuration.GetValue<string>("FaceitApiConfig:Url");
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IFaceitGateway, FaceitGateway>();
        services.AddTransient<AuthHeaderHandler>();
        services.AddRefitClient<IFaceitApi>()
            .AddHttpMessageHandler<AuthHeaderHandler>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(faceitUri);
            });
    }
}