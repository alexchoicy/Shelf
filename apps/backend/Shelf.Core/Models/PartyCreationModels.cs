using Shelf.Core.Enum;

namespace Shelf.Core.Models;

public sealed class PartyCreationRequest
{
    public required string Name { get; init; }
    public required PartyType PartyType { get; init; }
}

public sealed class PartyCreationResponse
{
    public required Guid PartyId { get; init; }
    public required string PartyName { get; init; }
    public required PartyType PartyType { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
