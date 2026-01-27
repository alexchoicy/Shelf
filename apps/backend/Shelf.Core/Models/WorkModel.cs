using Shelf.Core.Enum;

namespace Shelf.Core.Models;

public sealed class WorkCreationRequest
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    public string PrimaryPartyId { get; set; } = string.Empty;

    public bool IsAI { get; set; } = false;
    public string NovelContent { get; set; } = string.Empty;

    public WorkMedium Medium { get; set; }
    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;
    public WorkRating Rating { get; set; } = WorkRating.GENERAL;
    public DateTimeOffset? ReleasedAt { get; set; } = null;

    public required string CoverHash { get; set; } //blake3

    public List<MediaItemCreationRequest> MediaItems { get; set; } = new();
}

public sealed class MediaItemCreationRequest
{
    public required string FileHash { get; set; } //blake3
    public string Description { get; set; } = string.Empty;
    public MediaItemType MediaType { get; set; }
    public int Order { get; set; } = 0;
    public MediaItemKind Kind { get; set; } = MediaItemKind.MAIN;
}

public sealed class WorkCreationResponse
{
    public required Guid WorkId { get; set; }

    public required string CoverUploadURL { get; set; }
    public required List<MediaItemUploadInfo> MediaItems { get; set; }
}

public sealed class MediaItemUploadInfo
{
    public required Guid MediaItemId { get; set; }
    public required string FileHash { get; set; }
    public required string UploadURL { get; set; }
    public required string ThumbnailURL { get; set; }
}