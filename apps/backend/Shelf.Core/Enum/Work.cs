namespace Shelf.Core.Enum;

public enum WorkMedium
{
    Image,
    Video,
    Audio,
    Text,
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
    Public,
    Hidden,
}

public enum WorkRating
{
    General,
    Mature,
    Adult
}
