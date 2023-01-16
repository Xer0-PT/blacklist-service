namespace BlackList.Api.Controllers;

using BlackList.Application.Abstractions;
using BlackList.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CancellationToken cancellationToken)
    {
        var token = await _userService.CreateUserAsync(cancellationToken);

        return Ok(token);
    }
}