namespace Shelf.Core.Models;

public sealed class PartyAliasModel
{
    public required string AliasName { get; init; } = string.Empty;
    public required string AliasNormalizedName { get; init; } = string.Empty;
}

public sealed class PartyListModel
{
    public required Guid PartyId { get; init; }
    public required string PartyName { get; init; } = string.Empty;
    public required string PartyNormalizedName { get; init; } = string.Empty;
    public required IReadOnlyList<PartyAliasModel> PartyAliases { get; init; } = Array.Empty<PartyAliasModel>();
}