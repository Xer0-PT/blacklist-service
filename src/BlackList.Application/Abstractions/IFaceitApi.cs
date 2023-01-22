using BlackList.Application.Dtos;
using Refit;

namespace BlackList.Application.Abstractions;

public interface IFaceitApi
{
    [Get("/players?nickname={nickname}")]
    Task<FaceitUserDetails> GetUserDetailsAsync([AliasAs("nickname")] string nickname);
}