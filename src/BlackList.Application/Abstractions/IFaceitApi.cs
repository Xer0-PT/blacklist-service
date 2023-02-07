using Refit;

namespace BlackList.Application.Abstractions;

public interface IFaceitApi
{
    [Get("/players?nickname={nickname}")]
    Task<HttpResponseMessage> GetUserDetailsAsync([AliasAs("nickname")] string nickname, CancellationToken cancellationToken);
}