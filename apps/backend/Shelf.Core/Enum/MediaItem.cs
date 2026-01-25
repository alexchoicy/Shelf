namespace Shelf.Core.Enum;

public enum MediaItemType
{
    IMAGE,
    VIDEO,
    AUDIO
}

public enum MediaItemState
{
    PENDING,
    PROCESSING,
    COMPLETED,
    FAILED
}

public enum MediaItemKind
{
    MAIN,
    EXTRA
}