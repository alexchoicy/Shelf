using System;
using System.IO;
using Microsoft.Extensions.Options;
using Shelf.Core.Models;
using Shelf.Core.StorageServices;

namespace Shelf.Infrastructure.Services.Storage.Local;

public class LocalStorageProvider : IStorageProvider
{
    private readonly string _contentBaseUrl;

    public LocalStorageProvider(IOptions<StorageOptions> options)
    {
        LocalStorageOptions local = options?.Value?.Local ?? throw new InvalidOperationException("Local storage configuration is missing (Storage:Local).");

        _contentBaseUrl = (local.BaseUrl ?? string.Empty).TrimEnd('/');
    }

    public Task<string> CreatePresignedSourceUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/original/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }

    public Task<string> CreatePresignedCoverUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/cover/{Uri.EscapeDataString(key)}";
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

    public Task<string> CreatePresignedCoverDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default)
    {
        string url = $"{_contentBaseUrl}/local/cover/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }
}
