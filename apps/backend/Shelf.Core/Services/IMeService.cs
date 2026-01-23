using Shelf.Core.Models;

namespace Shelf.Core.Services;

public interface IMeService
{
    Task<UserInfo?> GetCurrentUserAsync(string userId);
}
