namespace Shelf.Core.Models;

public sealed class MediaFoldersOptions
{
    public string Original { get; init; } = "media/original";
    public string Transcode { get; init; } = "media/transcode";
    public string Thumbnail { get; init; } = "media/thumbnail";
    public string Preview { get; init; } = "media/preview";
    public string Other { get; init; } = "other";
}
