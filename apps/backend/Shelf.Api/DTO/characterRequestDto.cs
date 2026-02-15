namespace Shelf.Api.DTO;

public sealed class CreateCharacterRequest
{
    public required string Name { get; init; }
    public required Guid SeriesId { get; init; }
    public Guid? CreatorPartyId { get; init; }
}
