using Shelf.Core.StorageServices;
using Shelf.Core.Enum;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shelf.Infrastructure.StorageServices;

public interface IStorageProviderResolver
{
    IStorageProvider GetProvider(FileLocation location);
}

public class StorageProviderResolver : IStorageProviderResolver
{
    private readonly IServiceProvider _serviceProvider;

    public StorageProviderResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStorageProvider GetProvider(FileLocation location)
    {
        return location switch
        {
            FileLocation.SYSTEM => _serviceProvider.GetRequiredService<LocalStorageProvider>(),
            FileLocation.EXTERNAL => _serviceProvider.GetRequiredService<LocalStorageProvider>(),
            _ => _serviceProvider.GetRequiredService<LocalStorageProvider>(),
        };
    }
}
