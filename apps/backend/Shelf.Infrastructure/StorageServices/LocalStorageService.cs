using Shelf.Core.StorageServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Shelf.Infrastructure.StorageServices;

public class LocalStorageService : ILocalStorageService
{
    private readonly ILogger<LocalStorageService> _logger;

    public LocalStorageService(ILogger<LocalStorageService> logger)
    {
        _logger = logger;
    }

    public Task MarkAsync(Guid id, string? fileName, long size, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Marked local upload: {Id} filename={FileName} size={Size}", id, fileName, size);
        return Task.CompletedTask;
    }
}
