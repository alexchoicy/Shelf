namespace Shelf.Api.DTO;

public sealed class LoginRequestDto
{
    public required string Username { get; init; } = string.Empty;

    public required string Password { get; init; } = string.Empty;
}

public sealed class UserDto
{
    public required string Id { get; init; }

    public required string UserName { get; init; } = string.Empty;
    public required IList<string> Roles { get; init; } = new List<string>();
}

public sealed class LoginResponseDto
{
    public required string Token { get; init; } = string.Empty;

    public required UserDto User { get; init; }
}