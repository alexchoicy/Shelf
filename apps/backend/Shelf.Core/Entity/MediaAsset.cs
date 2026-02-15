using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class MediaAsset
{
    public int Id { get; set; }

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public string TextContent { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public MediaItemType MediaType { get; set; }

    public int Order { get; set; } = 0;

    public MediaItemKind Kind { get; set; } = MediaItemKind.Main;

    public ICollection<MediaVariant> Variants { get; set; } = new List<MediaVariant>();
}
