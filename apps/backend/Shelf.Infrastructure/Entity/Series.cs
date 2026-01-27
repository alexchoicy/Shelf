using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("Series")]
[PrimaryKey(nameof(Id))]
public class Series
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    public SeriesType Type { get; set; }

    private string _normalizedTitle = string.Empty;
    public string NormalizedTitle
    {
        get => _normalizedTitle;
        set => _normalizedTitle = value.Normalize();
    }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<SeriesWorks> SeriesWorks { get; set; } = new List<SeriesWorks>();
}