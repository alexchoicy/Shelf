using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Shelf.Infrastructure.Entity;

[Table("PartyAliases")]
[PrimaryKey(nameof(Id))]
public class PartyAlias
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

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