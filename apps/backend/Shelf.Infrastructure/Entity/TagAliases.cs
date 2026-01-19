using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("TagAliases")]
[PrimaryKey(nameof(Id))]
public class TagAlias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required int TagId { get; set; }
    public Tag? Tag { get; set; }

    public required string Name { get; set; }
    private string _normalizedName = string.Empty;
    public string NormalizedName
    {
        get => _normalizedName;
        set => _normalizedName = value.Normalize();
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}