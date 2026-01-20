namespace Shelf.Core.Models;

public sealed class UserInfo
{
    public required string Id { get; init; }

    public required string UserName { get; init; } = string.Empty;

    public required IList<string> Roles { get; init; } = new List<string>();
}

public sealed class AuthSession
{
    public required string Token { get; init; } = string.Empty;

    public required UserInfo User { get; init; }
}