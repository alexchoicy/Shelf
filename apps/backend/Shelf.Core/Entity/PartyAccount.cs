namespace Shelf.Core.Entity;

public class PartyAccount
{
    public int Id { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required int SourceId { get; set; }
    public Source? Source { get; set; }

    public string ExternalId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}
