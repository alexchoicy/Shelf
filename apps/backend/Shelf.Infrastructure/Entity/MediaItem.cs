using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;

namespace Shelf.Infrastructure.Entity;

[Table("MediaItems")]
[PrimaryKey(nameof(FileId), nameof(WorkId))]
public class MediaItem
{
    public required Guid FileId { get; set; }
    public File? File { get; set; }

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public string Description { get; set; } = string.Empty;
    public MediaItemType MediaType { get; set; }
    public int Order { get; set; } = 0;
    public MediaItemState State { get; set; } = MediaItemState.PENDING;
    public MediaItemKind Kind { get; set; } = MediaItemKind.MAIN;
}