using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelf.Infrastructure.Data;

namespace Shelf.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextPool<AppDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration["Database:ConnectionString"]);
        });

        return services;
    }
}
