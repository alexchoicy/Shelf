using Shelf.Core.StorageServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shelf.Core.Models;

namespace Shelf.Infrastructure.Services.Storage.Local;

public class LocalStorageService : ILocalStorageService
{
    private readonly ILogger<LocalStorageService> _logger;
    private readonly string _rootPath;

    public LocalStorageService(IOptions<StorageOptions> options, IOptions<MediaFoldersOptions> mediaFolders, ILogger<LocalStorageService> logger)
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

        _logger = logger;
    }

    public Task MarkAsync(Guid id, string? fileName, long size, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Marked local upload: {Id} filename={FileName} size={Size}", id, fileName, size);
        return Task.CompletedTask;
    }
}
