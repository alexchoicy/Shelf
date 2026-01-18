namespace Shelf.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string Normalize(this string value)
    {
        return value?.Trim().ToUpperInvariant() ?? string.Empty;
    }
}
