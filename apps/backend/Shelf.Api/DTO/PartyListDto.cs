namespace Shelf.Api.DTO;

public sealed class PartyAliasDto
{
    public required string AliasName { get; init; } = string.Empty;
    public required string AliasNormalizedName { get; init; } = string.Empty;
}

public sealed class PartyListDto
{
    public required Guid PartyId { get; init; }
    public required string PartyName { get; init; } = string.Empty;
    public required string PartyNormalizedName { get; init; } = string.Empty;
    public required IReadOnlyList<PartyAliasDto> PartyAliases { get; init; } = Array.Empty<PartyAliasDto>();
}