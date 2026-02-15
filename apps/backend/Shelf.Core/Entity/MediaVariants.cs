using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class MediaVariant
{
    public int Id { get; set; }

    public required int MediaAssetId { get; set; }
    public MediaAsset? MediaAsset { get; set; }

    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public int Priority { get; set; } = 0;
    //TODO: This will be set while doing the upload, because we need to make sure the uploaded files
    // are playable before setting default.
    // Source can be web unplayable formats/codec
    public bool IsDefault { get; set; } = false;

    public required MediaVariantPurpose Purpose { get; set; } = MediaVariantPurpose.Original;
    public string VariantKey { get; set; } = string.Empty;
    public int? TimeOffsetMs { get; set; } = null;
}
