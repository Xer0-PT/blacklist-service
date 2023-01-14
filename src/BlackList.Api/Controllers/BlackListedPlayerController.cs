namespace BlackList.Api.Controllers;

using BlackList.Api.Contracts;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BlackListedPlayerController : ControllerBase
{
    private readonly IBlackListedPlayerService _blackListService;

    public BlackListedPlayerController(IBlackListedPlayerService blackListService)
    {
        _blackListService = blackListService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BlackListedPlayerDto>>> GetAll(string token, CancellationToken cancellationToken)
    {
        var list = await _blackListService.GetAllBlackListedPlayersAsync(token, cancellationToken);

        if (list is null)
        {
            return NotFound("There is no user with requested token!");
        }

        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<BlackListedPlayerDto>> Create(CreateBlackListedPlayerQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _blackListService.CreateBlackListedPlayerAsync(query.Token, query.Nickname, cancellationToken);

            if (player is null)
            {
                return NotFound("There is no user with requested token!");
            }

            return Ok(player);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("This user already has this player blacklisted!");
        }
    }
}
