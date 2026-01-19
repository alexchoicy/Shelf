using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shelf.Infrastructure.Entity;

[Table("Files")]
[PrimaryKey(nameof(Id))]
public class File
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Hash { get; set; }

    public required long FileSize { get; set; }
    public required string MimeType { get; set; }

    public required int Width { get; set; }
    public required int Height { get; set; }

    public int? DurationInSeconds { get; set; } = null;
    public string Codec { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;

    public ICollection<MediaItem> MediaItems { get; set; } = new List<MediaItem>();
    public ICollection<Work> CoverWorks { get; set; } = new List<Work>();
    public ICollection<Party> CoverParties { get; set; } = new List<Party>();
}