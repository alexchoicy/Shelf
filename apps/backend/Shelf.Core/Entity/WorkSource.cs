

namespace Shelf.Core.Entity;

public class WorkSource
{
    public int Id { get; set; }

    public required Guid WorkId { get; set; }
    public Work Work { get; set; } = default!;

    public required int SourceId { get; set; }
    public Source Source { get; set; } = default!;

    public string ExternalId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public DateTimeOffset? ReleasedAt { get; set; }

    public DateTimeOffset FetchedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
