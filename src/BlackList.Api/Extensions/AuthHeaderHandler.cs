using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace BlackList.Api.Extensions;

[ExcludeFromCodeCoverage]
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;

    public AuthHeaderHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authToken = _configuration.GetValue<string>("FaceitApiConfig:AuthToken");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        return base.SendAsync(request, cancellationToken);
    }
}