namespace Shelf.Core.Models;

public sealed record MultipartUploadInfo
{
    public required string UploadId { get; init; }
    public required string FileID { get; init; }
    public required long PartSizeInBytes { get; init; }
    public required IReadOnlyList<MultipartUploadPartInfo> Parts { get; init; }
}

public sealed record MultipartUploadPartInfo
{
    public required int PartNumber { get; init; }
    public required string Url { get; init; }
}

public sealed class CompleteMultipartUploadRequest
{
    public required string FileID { get; init; }
    public required string UploadId { get; init; }
    public required List<CompleteMultipartUploadPart> Parts { get; init; }
}

public sealed class CompleteMultipartUploadPart
{
    public required int PartNumber { get; init; }
    public required string ETag { get; init; }
}
