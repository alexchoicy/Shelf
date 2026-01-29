namespace Shelf.Core.Utils;

public static class Storage
{
    public static string GeneratePathFromHash(string hash)
    {
        string firstSubPath = hash.Substring(0, 2);
        string secondSubPath = hash.Substring(2, 2);
        return Path.Combine(firstSubPath, secondSubPath);
    }
}
