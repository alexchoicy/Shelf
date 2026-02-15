namespace Shelf.Core.Entity;

public class PartyAlias
{
    public int Id { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required string Name { get; set; }

    public string NormalizedName { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
