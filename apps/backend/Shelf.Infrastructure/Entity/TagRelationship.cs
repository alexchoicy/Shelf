using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;

namespace Shelf.Infrastructure.Entity;

[Table("TagRelationships")]
[PrimaryKey(nameof(Id))]
public class TagRelationship
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int PrimaryTagId { get; set; }
    public Tag PrimaryTag { get; set; } = null!;

    public int RelatedTagId { get; set; }
    public Tag RelatedTag { get; set; } = null!;

    public TagRelationshipType RelationshipType { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }
}
