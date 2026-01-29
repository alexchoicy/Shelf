namespace Shelf.Core.Models;

public sealed class LocalStorageOptions
{
    public required string RootPath { get; init; }
    public string? BaseUrl { get; init; }
}

public sealed class S3StorageOptions
{
    public required string Bucket { get; init; }
    public string? Region { get; init; }
    public string? AccessKey { get; init; }
    public string? SecretKey { get; init; }
    public string? Endpoint { get; init; }
    public bool UsePathStyle { get; init; } = false;
    public string? BasePath { get; init; }
}

public sealed class StorageOptions
{
    public required string DefaultProvider { get; init; } = "Local";
    public LocalStorageOptions? Local { get; init; }
    public S3StorageOptions? S3 { get; init; }
    public MediaFoldersOptions MediaFolders { get; init; } = new();
}
