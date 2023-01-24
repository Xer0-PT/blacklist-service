using System.Net.Http.Json;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;

namespace BlackList.Application.Services;

public class FaceitGateway : IFaceitGateway
{
    private readonly IFaceitApi _faceitApi;

    public FaceitGateway(IFaceitApi faceitApi)
    {
        _faceitApi = faceitApi;
    }

    public async Task<Guid> GetFaceitIdAsync(string nickname, CancellationToken cancellationToken)
    {
        var response = await _faceitApi.GetUserDetailsAsync(nickname);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentNullException(nickname, "This player does not exist!");
        }

        var playerDetails = await response.Content.ReadFromJsonAsync<FaceitUserDetails>(cancellationToken: cancellationToken);

        return playerDetails!.PlayerId;
    }
}