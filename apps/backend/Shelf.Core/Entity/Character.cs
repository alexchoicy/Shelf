namespace Shelf.Core.Entity;

public class Character
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Name { get; set; }

    public string NormalizedName { get; set; } = string.Empty;

    public Guid? CreatorPartyId { get; set; } // for original
    public Party? CreatorParty { get; set; }

    public Guid SeriesId { get; set; }
    public Series? Series { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<CharacterAlias> Aliases { get; set; } = new List<CharacterAlias>();
    public ICollection<WorkCharacter> WorkCharacters { get; set; } = new List<WorkCharacter>();
}
