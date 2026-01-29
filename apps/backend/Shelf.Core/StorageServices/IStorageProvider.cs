
namespace Shelf.Core.StorageServices;

public interface IStorageProvider
{
    Task<string> CreatePresignedUploadAsync(string key, TimeSpan? expires = null, string? contentType = null, CancellationToken cancellationToken = default);

    Task<string> CreatePresignedSourceDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default);
    Task<string> CreatePresignedTranscodeDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default);
    Task<string> CreatePresignedThumbnailDownloadAsync(string key, TimeSpan? expires = null, CancellationToken cancellationToken = default);
}
