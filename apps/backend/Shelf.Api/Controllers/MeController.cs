using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Api.DTO;
using Shelf.Core.Services;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("me")]
[Authorize]
public class MeController : ControllerBase
{
    private readonly IMeService _meService;

    public MeController(IMeService meService) => _meService = meService;

    [HttpGet]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var userInfo = await _meService.GetCurrentUserAsync(userId);

        if (userInfo is null)
        {
            return Unauthorized();
        }

        return Ok(new UserDto
        {
            Id = userInfo.Id,
            UserName = userInfo.UserName,
            Roles = userInfo.Roles
        });
    }
}
