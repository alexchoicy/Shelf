using Shelf.Core.Enum;

namespace Shelf.Core.Models;

public sealed class WorkCreationCredit
{
    public required Guid PartyId { get; init; }

    public required WorkCreditRole Role { get; init; }
}

public sealed class WorkCreationRequest
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    public required List<WorkCreationCredit> Credits { get; init; } = new();

    public bool IsAI { get; set; } = false;
    public string NovelContent { get; set; } = string.Empty;

    public WorkMedium Medium { get; set; }
    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;
    public WorkRating Rating { get; set; } = WorkRating.GENERAL;
    public DateTimeOffset? ReleasedAt { get; set; } = null;

    public string? CoverHash { get; set; } //blake3
    public string? CoverMimeType { get; set; }
    public int? CoverWidth { get; set; }
    public int? CoverHeight { get; set; }
    public long? CoverFileSize { get; set; }

    public List<MediaItemCreationRequest> MediaItems { get; set; } = new();
}

public sealed class MediaItemCreationRequest
{
    public string? FileHash { get; set; } //blake3
    public string Description { get; set; } = string.Empty;
    public required MediaItemType MediaType { get; set; }
    public string TextContent { get; set; } = string.Empty;
    public required string MimeType { get; set; }
    public required long FileSize { get; set; }
    public required int Order { get; set; }
    public required MediaItemKind Kind { get; set; } = MediaItemKind.MAIN;
    public int? Width { get; set; }
    public int? Height { get; set; }
}

public sealed class WorkCreationResponse
{
    public required Guid WorkId { get; set; }
    public string? CoverUploadURL { get; set; }
    public required List<MediaItemUploadInfo> MediaItems { get; set; }
}

public sealed class MediaItemUploadInfo
{
    public required Guid MediaItemId { get; set; }
    public required string FileHash { get; set; }
    public required string UploadURL { get; set; }
}
