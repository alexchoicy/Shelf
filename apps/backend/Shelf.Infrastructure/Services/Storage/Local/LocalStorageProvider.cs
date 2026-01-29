using System;
using System.IO;
using Microsoft.Extensions.Options;
using Shelf.Core.Models;
using Shelf.Core.StorageServices;

namespace Shelf.Infrastructure.Storage.Local;

public class LocalStorageProvider : IStorageProvider
{
    private readonly string _contentBaseUrl;
    private readonly string _rootPath;

    public LocalStorageProvider(IOptions<StorageOptions> options, IOptions<MediaFoldersOptions> mediaFolders)
    {
        LocalStorageOptions local = options?.Value?.Local ?? throw new InvalidOperationException("Local storage configuration is missing (Storage:Local).");

        if (string.IsNullOrWhiteSpace(local.RootPath))
            throw new InvalidOperationException("Storage:Local:RootPath is required.");

        _rootPath = Path.GetFullPath(local.RootPath);

        try
        {
            Directory.CreateDirectory(_rootPath);
            Directory.CreateDirectory(Path.Combine(_rootPath, mediaFolders.Value.Original));
            Directory.CreateDirectory(Path.Combine(_rootPath, mediaFolders.Value.Transcode));
            Directory.CreateDirectory(Path.Combine(_rootPath, mediaFolders.Value.Thumbnail));
            Directory.CreateDirectory(Path.Combine(_rootPath, mediaFolders.Value.Preview));
            Directory.CreateDirectory(Path.Combine(_rootPath, mediaFolders.Value.Other));
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Cannot create or access local storage root path '{_rootPath}'.", ex);
        }

        _contentBaseUrl = (local.BaseUrl ?? string.Empty).TrimEnd('/');
    }

    public Task<string> CreatePresignedUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/original/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }

    public Task<string> CreatePresignedSourceDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/original/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }

    public Task<string> CreatePresignedTranscodeDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/transcode/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }

    public Task<string> CreatePresignedThumbnailDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/thumbnail/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }
}
