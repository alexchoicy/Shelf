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
    public required List<Guid> CharacterIds { get; init; } = new();

    public string NovelContent { get; init; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkMedium Medium { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkType Type { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkVisibility Visibility { get; init; } = WorkVisibility.Public;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required WorkRating Rating { get; init; } = WorkRating.General;
    public required bool IsAI { get; init; } = false;
    public DateTimeOffset? ReleasedAt { get; init; } = null;

    public required List<MediaItemCreationRequest> MediaItems { get; init; } = new();
}

public sealed class MediaItemCreationRequest
{
    public string? SimpleBlake3 { get; init; } // blake3 or nothing if text
    public string Description { get; init; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemType MediaType { get; init; }

    public string TextContent { get; init; } = string.Empty;

    public required int Order { get; init; }

    public string? MimeType { get; init; }
    public long? FileSize { get; init; }
    public string? OriginalFileName { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MediaItemKind Kind { get; init; } = MediaItemKind.Main;
}
