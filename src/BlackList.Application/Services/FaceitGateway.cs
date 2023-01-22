using BlackList.Application.Abstractions;

namespace BlackList.Application.Services;

public class FaceitGateway : IFaceitGateway
{
    private readonly IFaceitApi _faceitApi;

    public FaceitGateway(IFaceitApi faceitApi)
    {
        _faceitApi = faceitApi;
    }

    public async Task<Guid> GetFaceitIdAsync(string nickname, CancellationToken cancellationToken)
        => (await _faceitApi.GetUserDetailsAsync(nickname)).PlayerId;
    }