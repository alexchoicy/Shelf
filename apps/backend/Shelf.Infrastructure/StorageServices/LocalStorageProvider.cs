using Microsoft.Extensions.Configuration;
using Shelf.Core.StorageServices;

namespace Shelf.Infrastructure.StorageServices;

public class LocalStorageProvider(IConfiguration configuration) : IStorageProvider
{
    private readonly string _contentBaseUrl = configuration?["Storage:ContentBaseUrl"]!.TrimEnd('/')!;

    public Task<string> CreatePresignedUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default)
    {
        var url = $"{_contentBaseUrl}/local/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }

    // Returns a stable content URL (no expiry) like: {contentBaseUrl}/local/{id}
    public Task<string> CreatePresignedDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default)
    {
        var url = $"{_contentBaseUrl}/local/{Uri.EscapeDataString(key)}";
        return Task.FromResult(url);
    }
}

