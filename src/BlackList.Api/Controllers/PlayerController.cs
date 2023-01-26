using BlackList.Api.Contracts;
using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlackList.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlayerDto>>> GetAll(Guid userFaceitId, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _playerService.GetAllPlayersAsync(userFaceitId, cancellationToken);

            return Ok(list);
        }
        catch(ArgumentNullException ex) 
        { 
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PlayerDto>> Create(CreatePlayerQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var player = await _playerService.CreatePlayerAsync(query.UserFaceitId, query.PlayerNickname, cancellationToken);

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
    public async Task<ActionResult<PlayerDto>> Update(UpdatePlayerQuery query, CancellationToken cancellationToken) 
    {
        try 
        {
            var player = await _playerService.UndoPlayerBanAsync(query.UserFaceitId, query.PlayerNickname, cancellationToken);
            return Ok(player);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
