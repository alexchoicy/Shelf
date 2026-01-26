namespace Shelf.Core.Enum;

public enum WorkMedium
{
    IMAGE,
    VIDEO,
    AUDIO,
    TEXT
}


public enum WorkType
{
    ILLUSTRATION,
    MANGA,
    WEBTOON,
    COMIC,
    PHOTOSET,

    ANIME,
    OVA,
    PMV,
    HMV,
    CONCERT,
    VIDEO,

    MUSIC,
    ASMR,

    NOVEL,
    LIGHT_NOVEL,
    TEXTONLY,

    PORN, // Others maybe categorize by tags
    JAV // I have no idea how to categorize other than JAV
}


public enum WorkVisibility
{
    PUBLIC,
    HIDDEN,
}

public enum WorkRating
{
    GENERAL,
    MATURE,
    ADULT
}