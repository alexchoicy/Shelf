using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;

namespace Shelf.Infrastructure.Entity;

[Table("MediaAssets")]
[PrimaryKey(nameof(Id))]
public class MediaAsset
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public string Description { get; set; } = string.Empty;
    public MediaItemType MediaType { get; set; }
    public int Order { get; set; } = 0;
    public MediaItemState State { get; set; } = MediaItemState.PENDING;
    public MediaItemKind Kind { get; set; } = MediaItemKind.MAIN;

    public ICollection<MediaVariant> Variants { get; set; } = new List<MediaVariant>();
}