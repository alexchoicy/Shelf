using System.Text.Json.Serialization;
using Shelf.Core.Enum;

namespace Shelf.Api.DTO;

public sealed class WorkCreationRequest
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    public required string PrimaryPartyId { get; set; } = string.Empty;

    public required bool IsAI { get; set; } = false;
    public string NovelContent { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkMedium Medium { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkRating Rating { get; set; } = WorkRating.GENERAL;
    public DateTime? ReleasedAt { get; set; } = null;

    public required string CoverHash { get; set; } //blake3

    public required List<MediaItemCreationRequest> MediaItems { get; set; }
}

public sealed class MediaItemCreationRequest
{
    public required string FileHash { get; set; } //blake3

    // I think thumbnail is optional for videos
    public string? ThumbnailHash { get; set; }
    public string Description { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemType MediaType { get; set; }
    public required string MimeType { get; set; }
    public required long FileSize { get; set; }
    public required int Order { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemKind Kind { get; set; } = MediaItemKind.MAIN;
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

    // This will be hardcoded with a style and size and file type
    public required string ThumbnailURL { get; set; }
}