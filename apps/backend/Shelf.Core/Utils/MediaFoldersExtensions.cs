using Shelf.Core.Enum;
using Shelf.Core.Models;

namespace Shelf.Core.Utils;

public static class MediaFoldersExtensions
{
    public static string GetFolder(this MediaVariantPurpose variant, MediaFoldersOptions folders) =>
        variant switch
        {
            MediaVariantPurpose.Original => folders.Original,
            MediaVariantPurpose.Transcoded => folders.Transcode,
            MediaVariantPurpose.Thumbnail => folders.Thumbnail,
            MediaVariantPurpose.Preview => folders.Preview,
            MediaVariantPurpose.Cover => folders.Cover,
            _ => folders.Other
        };
}
