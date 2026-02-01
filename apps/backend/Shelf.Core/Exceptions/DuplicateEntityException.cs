namespace Shelf.Core.Exceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string entity, string? message = null)
        : base(message ?? $"{entity} already exists.")
    {
    }
}
