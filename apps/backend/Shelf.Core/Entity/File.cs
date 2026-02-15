using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class File
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    // Since client blake3 is slow, i think we check dup with OriginalFileName + SizeInByte
    public string? Blake3Hash { get; set; }

    public FileProcessingStatus ProcessingStatus { get; set; } = FileProcessingStatus.Pending;

    public required string StoragePath { get; set; }

    public required FileObjectVariant FileObjectVariant { get; set; }

    public required long SizeInBytes { get; set; }
    public required string MimeType { get; set; }

    public string? Container { get; set; }
    public string? Codec { get; set; }

    public int? Width { get; set; }
    public int? Height { get; set; }

    public int? DurationInMs { get; set; }
    public decimal? FrameRate { get; set; }
    public int? Bitrate { get; set; }

    public required string OriginalFileName { get; set; }
    public string? CreatedByUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<MediaVariant> MediaVariants { get; set; } = new List<MediaVariant>();
    public ICollection<PartyCover> PartyCovers { get; set; } = new List<PartyCover>();
    public ICollection<WorkCover> WorkCovers { get; set; } = new List<WorkCover>();
}
