using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("WorkTags")]
[PrimaryKey(nameof(WorkId), nameof(TagId))]
public class WorkTag
{
    public required Guid WorkId { get; set; }
    public Work Work { get; set; } = default!;

    public required int TagId { get; set; }
    public Tag Tag { get; set; } = default!;

    public int AssertedByWorkSourceId { get; set; }
    public WorkSource AssertedByWorkSource { get; set; } = default!;

    public string AssertedByUserId { get; set; }
    public User AssertedByUser { get; set; } = default!;

    public int AssertedByModelId { get; set; }
    public Model AssertedByModel { get; set; } = default!;

    public float Confidence { get; set; } = 0.0f;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}