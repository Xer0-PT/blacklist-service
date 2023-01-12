namespace BlackList.Api.Controllers;

using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BlackListController : ControllerBase
{
    private readonly IBlackListService _blackListService;

    public BlackListController(IBlackListService blackListService)
    {
        _blackListService = blackListService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BlackListedPlayerDto>>> GetAll(string token, CancellationToken cancellationToken)
    {
        var list = await _blackListService.GetAllBlackListedPlayersAsync(token, cancellationToken);

        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<BlackListedPlayerDto>> Create(string nickname, CancellationToken cancellationToken)
    {
        var player = await _blackListService.CreateBlackListedPlayerAsync(nickname, cancellationToken);

        return Ok(player);
    }
}
