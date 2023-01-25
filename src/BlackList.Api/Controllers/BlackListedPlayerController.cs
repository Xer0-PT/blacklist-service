namespace BlackList.Api.Controllers;

using BlackList.Api.Contracts;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class BlackListedPlayerController : ControllerBase
{
    private readonly IBlackListedPlayerService _blackListService;

    public BlackListedPlayerController(IBlackListedPlayerService blackListService)
    {
        _blackListService = blackListService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BlackListedPlayerDto>>> GetAll(Guid userFaceitId, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _blackListService.GetAllBlackListedPlayersAsync(userFaceitId, cancellationToken);

            return Ok(list);
        }
        catch(ArgumentNullException ex) 
        { 
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<BlackListedPlayerDto>> Create(CreateBlackListedPlayerQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _blackListService.CreateBlackListedPlayerAsync(query.UserFaceitId, query.PlayerNickname, cancellationToken);

            return Ok(player);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut]
    public async Task<ActionResult<BlackListedPlayerDto>> Update(UpdateBlackListedPlayerQuery query, CancellationToken cancellationToken) 
    {
        try 
        {
            var player = await _blackListService.UndoPlayerBanAsync(query.UserFaceitId, query.PlayerNickname, cancellationToken);
            return Ok(player);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
