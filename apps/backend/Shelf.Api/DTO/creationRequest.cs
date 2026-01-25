using Shelf.Infrastructure.Enum;

namespace Shelf.Api.DTO;

public sealed class WorkCreationRequest
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsAI { get; set; } = false;
    public string NovelContent { get; set; } = string.Empty;

    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;
    public WorkRating Rating { get; set; } = WorkRating.GENERAL;
    public DateTime? ReleasedAt { get; set; } = null;

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