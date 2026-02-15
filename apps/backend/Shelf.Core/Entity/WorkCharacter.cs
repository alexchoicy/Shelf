
using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class WorkCharacter
{
    public int Id { get; set; }

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public required Guid CharacterId { get; set; }
    public Character? Character { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}
