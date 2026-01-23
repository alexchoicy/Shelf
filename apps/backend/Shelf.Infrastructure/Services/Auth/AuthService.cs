using Microsoft.AspNetCore.Identity;
using Shelf.Core.Models;
using Shelf.Infrastructure.Authentication;
using Shelf.Infrastructure.Entity;

namespace Shelf.Core.Services;



public class AuthService : IAuthService
{

    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthSession?> LoginAsync(string username, string password)
    {
        User? user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return null;
        }

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            return null;
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        string token = _tokenService.GenerateUserToken(user, roles);

        return new AuthSession
        {
            Token = token,
            User = new UserInfo
            {
                Id = user.Id,
                UserName = user.UserName!,
                Roles = roles
            }
        };
    }
}