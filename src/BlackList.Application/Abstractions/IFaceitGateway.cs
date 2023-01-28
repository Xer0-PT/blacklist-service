namespace BlackList.Application.Abstractions;

public interface IFaceitGateway
{
    Task<Guid> GetFaceitIdAsync(string nickname, CancellationToken cancellationToken);
}