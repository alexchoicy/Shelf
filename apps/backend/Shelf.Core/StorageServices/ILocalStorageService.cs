using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shelf.Core.StorageServices;

public interface ILocalStorageService
{
    Task MarkAsync(Guid id, string? fileName, long size, CancellationToken cancellationToken = default);
}
