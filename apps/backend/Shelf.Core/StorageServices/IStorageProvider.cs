
namespace Shelf.Core.StorageServices;

public interface IStorageProvider
{
    Task<string> CreatePresignedUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default);

    Task<string> CreatePresignedDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default);
}
