using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;

namespace Shelf.Infrastructure.Entity;

[Table("MediaVariants")]
[PrimaryKey(nameof(Id))]
public class MediaVariant
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required Guid MediaAssetId { get; set; }
    public MediaAsset? MediaAsset { get; set; }

    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public required bool IsDefault { get; set; }

    public required MediaVariantPurpose Purpose { get; set; } = MediaVariantPurpose.ORIGINAL;
    public string VariantKey { get; set; } = string.Empty;
}