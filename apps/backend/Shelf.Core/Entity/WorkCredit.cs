
using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class WorkCredit
{
    public int Id { get; set; }

    public Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required WorkCreditRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}
