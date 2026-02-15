using System.Text.Json.Serialization;
using Shelf.Core.Enum;

namespace Shelf.Core.Models;

public sealed class LocalStorageOptions
{
    public required string RootPath { get; init; }
    public string? BaseUrl { get; init; }
}

public sealed class S3StorageOptions
{
    public required string AccessURL { get; init; }
    public required string BucketName { get; init; }
    public string Region { get; init; } = "auto";
    public string? Endpoint { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
}


public sealed class StorageOptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required StorageProvider DefaultProvider { get; init; } = StorageProvider.S3;
    public LocalStorageOptions? Local { get; init; }
    public S3StorageOptions? S3 { get; init; }
    public MediaFoldersOptions MediaFolders { get; init; } = new();
}

public sealed class MediaFoldersOptions
{
    public string Original { get; init; } = "media/original";
    public string Transcode { get; init; } = "media/transcode";
    public string Thumbnail { get; init; } = "media/thumbnail";
    public string Preview { get; init; } = "media/preview";
    public string Cover { get; init; } = "media/cover";
    public string Other { get; init; } = "other";
}
