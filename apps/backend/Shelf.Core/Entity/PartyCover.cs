
namespace Shelf.Core.Entity;

public class PartyCover
{
    public int Id { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public bool IsCurrent { get; set; } = true;
    public string? Note { get; set; }

    public string? SetByUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
