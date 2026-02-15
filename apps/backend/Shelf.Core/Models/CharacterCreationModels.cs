namespace Shelf.Core.Models;

public sealed class CharacterCreationRequest
{
    public required string Name { get; init; }
    public required Guid SeriesId { get; init; }
    public Guid? CreatorPartyId { get; init; }
}
