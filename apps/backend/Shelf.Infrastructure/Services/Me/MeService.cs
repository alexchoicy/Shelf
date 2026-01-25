using Microsoft.AspNetCore.Identity;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Services;

public class MeService : IMeService
{
    private readonly UserManager<User> _userManager;

    public MeService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserInfo?> GetCurrentUserAsync(string userId)
    {
        User? user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return null;
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        return new UserInfo
        {
            Id = user.Id,
            UserName = user.UserName!,
            Roles = roles
        };
    }
}
