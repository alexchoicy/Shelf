namespace Shelf.Core.Entity;

public class CharacterAlias
{
    public int Id { get; set; }

    public required Guid CharacterId { get; set; }
    public Character? Character { get; set; }

    public required string Name { get; set; }

    public string NormalizedName = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
