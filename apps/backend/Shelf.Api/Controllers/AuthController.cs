using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Api.DTO;
using Shelf.Core.Models;
using Shelf.Infrastructure.Authentication;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private const string AuthCookieName = "AlexCoolShelfAppToken";
    private readonly IWebHostEnvironment _environment;
    private readonly IAuthService _authService;

    public AuthController(
        IWebHostEnvironment environment, IAuthService authService)
    {
        _environment = environment;
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Email/username and password are required." });
        }

        AuthSession? result = await _authService.LoginAsync(request.Username, request.Password);

        if (result is null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        Response.Cookies.Append(AuthCookieName, result.Token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Secure = !_environment.IsDevelopment(),
            IsEssential = true,
        });

        return Ok(new LoginResponseDto
        {
            Token = result.Token,
            User = new UserDto
            {
                Id = result.User.Id,
                UserName = result.User.UserName,
                Roles = result.User.Roles,
            },
        });
    }
}
