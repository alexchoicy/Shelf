using System.Text.Json.Serialization;
using Shelf.Core.Enum;

namespace Shelf.Api.DTO;

public sealed class WorkCreationCredit
{
    public required Guid PartyId { get; init; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkCreditRole Role { get; init; }
}

public sealed class WorkCreationRequest
{
    public required string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public required List<WorkCreationCredit> Credits { get; init; } = new();

    public string NovelContent { get; init; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkMedium Medium { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkType Type { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkVisibility Visibility { get; init; } = WorkVisibility.PUBLIC;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkRating Rating { get; init; } = WorkRating.GENERAL;
    public required bool IsAI { get; init; } = false;
    public DateTimeOffset? ReleasedAt { get; init; } = null;

    public string? CoverHash { get; init; }
    public string? CoverMimeType { get; init; }
    public int? CoverWidth { get; init; }
    public int? CoverHeight { get; init; }

    public required List<MediaItemCreationRequest> MediaItems { get; init; } = new();
}

public sealed class MediaItemCreationRequest
{
    public required string FileHash { get; init; } // blake3 or nothing if text
    public string Description { get; init; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemType MediaType { get; init; }

    public string TextContent { get; init; } = string.Empty;

    public required string MimeType { get; init; } = string.Empty;
    public required long FileSize { get; init; }
    public required int Order { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemKind Kind { get; init; } = MediaItemKind.MAIN;

    public int? Width { get; init; }
    public int? Height { get; init; }
}




public sealed class WorkCreationResponse
{
    public required Guid WorkId { get; init; }
    public string? CoverUploadURL { get; init; }
    public required List<MediaItemUploadInfo> MediaItems { get; init; } = new();
}

public sealed class MediaItemUploadInfo
{
    public required Guid MediaItemId { get; init; }
    public required string FileHash { get; init; }
    public required string UploadURL { get; init; }
}
