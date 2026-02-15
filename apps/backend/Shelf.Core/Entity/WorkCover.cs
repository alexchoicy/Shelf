
namespace Shelf.Core.Entity;

public class WorkCover
{
    public int Id { get; set; }

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public bool IsCurrent { get; set; } = true;
    public string? Note { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
