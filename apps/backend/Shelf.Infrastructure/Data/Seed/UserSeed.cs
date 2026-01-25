using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shelf.Infrastructure.Entity;
using Shelf.Core.Enum;

namespace Shelf.Infrastructure.Data.Seed;

public class UserSeed
{
    public static async Task SeedAsync(UserManager<User> userManager, IConfiguration configuration)
    {
        string adminName = "admin";
        User? adminUser = await userManager.FindByNameAsync(adminName);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminName,
            };

            IdentityResult result = await userManager.CreateAsync(adminUser, configuration["ADMIN"] ?? "admin");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
            }
        }
    }
}