using Shelf.Core.Enum;
using Shelf.Core.Models;
using Shelf.Core.Services;

namespace Shelf.Infrastructure.Services.Storage;

public class S3StorageService() : IStorageService
{
    // private readonly string bucket = options.Value.Content!.S3!.BucketName;

    public Task CompleteMultipartUploadAsync(List<CompleteMultipartUploadRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MultipartUploadInfo> CreateMultipartUploadAsync(string objectPath, string mimeType, long fileSizeInBytes, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string GetStoragePath(MediaVariantPurpose variant, Guid fileID, string mimeType)
    {
        throw new NotImplementedException();
    }

}
