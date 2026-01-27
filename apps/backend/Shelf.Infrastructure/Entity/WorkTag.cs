using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("WorkTags")]
[PrimaryKey(nameof(WorkId), nameof(TagId))]
public class WorkTag
{
    public required Guid WorkId { get; set; }
    public Work Work { get; set; } = default!;

    public required int TagId { get; set; }
    public Tag Tag { get; set; } = default!;

    public int? AssertedByWorkSourceId { get; set; }
    public WorkSource? AssertedByWorkSource { get; set; }

    public string? AssertedByUserId { get; set; }
    public User? AssertedByUser { get; set; }

    public int? AssertedByModelId { get; set; }
    public AIModel? AssertedByModel { get; set; }

    public float Confidence { get; set; } = 0.0f;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}