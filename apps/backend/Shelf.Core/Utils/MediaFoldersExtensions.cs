using Shelf.Core.Enum;
using Shelf.Core.Models;

namespace Shelf.Infrastructure.Storage;

public static class MediaFoldersExtensions
{
    public static string GetFolder(this MediaVariantPurpose variant, MediaFoldersOptions folders) =>
        variant switch
        {
            MediaVariantPurpose.ORIGINAL => folders.Original,
            MediaVariantPurpose.TRANSCODE => folders.Transcode,
            MediaVariantPurpose.THUMBNAIL => folders.Thumbnail,
            MediaVariantPurpose.PREVIEW => folders.Preview,
            _ => folders.Other
        };
}
