namespace Shelf.Core.Models;

public sealed class CharacterAliasModel
{
    public required string AliasName { get; init; }
    public required string AliasNormalizedName { get; init; }
}

public sealed class CharacterListModel
{
    public required Guid CharacterId { get; init; }
    public required string CharacterName { get; init; }
    public required string CharacterNormalizedName { get; init; }
    public required Guid SeriesId { get; init; }
    public Guid? CreatorPartyId { get; init; }
    public required List<CharacterAliasModel> CharacterAliases { get; init; }
}
