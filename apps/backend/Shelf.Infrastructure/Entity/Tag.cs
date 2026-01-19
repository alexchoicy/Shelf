using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("Tags")]
[PrimaryKey(nameof(Id))]
public class Tag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    private string _normalizedName = string.Empty;
    public string NormalizedName
    {
        get => _normalizedName;
        set => _normalizedName = value.Normalize();
    }

    public string Description { get; set; } = string.Empty;

    public string Namespace { get; set; } = string.Empty;

    public int SourceId { get; set; }
    public Source Source { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    public ICollection<TagAlias> Aliases { get; set; } = new List<TagAlias>();
    public ICollection<WorkTag> WorkTags { get; set; } = new List<WorkTag>();
}