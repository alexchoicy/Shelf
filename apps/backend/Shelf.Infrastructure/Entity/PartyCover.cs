using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shelf.Infrastructure.Entity;

[Table("PartyCovers")]
[PrimaryKey(nameof(Id))]
public class PartyCover
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public bool IsCurrent { get; set; } = true;

    public string? Note { get; set; }

    public string? SetByUserId { get; set; }
    public User? SetByUser { get; set; }

    public int? SetBySourceId { get; set; }
    public Source? SetBySource { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}
