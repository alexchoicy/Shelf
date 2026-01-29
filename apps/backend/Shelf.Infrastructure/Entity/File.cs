using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;

namespace Shelf.Infrastructure.Entity;

[Table("Files")]
[PrimaryKey(nameof(Id))]
public class File
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Hash { get; set; }

    //TODO phash

    // i dunno this little guy may be useful later
    // defualt the files is the Guid as the filename
    public FileLocation Location { get; set; } = FileLocation.SYSTEM;

    // Relative path from the storage root
    public string? Path { get; set; }

    public required long FileSize { get; set; }
    public required string MimeType { get; set; }

    //I think these two are possible to be 0
    public required int Width { get; set; }
    public required int Height { get; set; }

    public int? DurationInSeconds { get; set; } = null;
    public string Codec { get; set; } = string.Empty;
    public int? Bitrate { get; set; }

    public decimal? FrameRate { get; set; }
    public int? AudioSampleRate { get; set; }
    public int? AudioChannels { get; set; }


    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<MediaVariant> MediaVariants { get; set; } = new List<MediaVariant>();

    public ICollection<WorkCover> WorkCovers { get; set; } = new List<WorkCover>();
    public ICollection<PartyCover> PartyCovers { get; set; } = new List<PartyCover>();
}