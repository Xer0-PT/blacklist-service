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
    public async Task<ActionResult<UserDto>> CreateUser(string nickname, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(nickname, cancellationToken);

        return Ok(user);
    }
}