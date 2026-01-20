using Shelf.Core.Models;

namespace Shelf.Infrastructure.Authentication;


public interface IAuthService
{
    Task<AuthSession?> LoginAsync(string emailOrUserName, string password);
}
