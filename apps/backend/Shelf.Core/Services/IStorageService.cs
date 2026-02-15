using Shelf.Core.Enum;
using Shelf.Core.Models;
namespace Shelf.Core.Services;

public interface IStorageService
{
    public string GetStoragePath(MediaVariantPurpose variant, Guid fileID, string mimeType);

    public Task<MultipartUploadInfo> CreateMultipartUploadAsync(
        string objectPath,
        string mimeType,
        long fileSizeInBytes,
        CancellationToken cancellationToken = default);

    public Task CompleteMultipartUploadAsync(List<CompleteMultipartUploadRequest> requests, CancellationToken cancellationToken = default);
}
