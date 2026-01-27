namespace Shelf.Core.Enum;

public enum MediaItemType
{
    IMAGE,
    VIDEO,
    AUDIO,
    TEXT
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