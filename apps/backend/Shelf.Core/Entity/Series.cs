namespace Shelf.Core.Entity;

// TODO: Seed Original Series by a party
// This is for group of IPs for characters, workSeries in other table, later.
public class Series
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Name { get; set; }

    public string NormalizedName = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<Character> Characters { get; set; } = [];
}
