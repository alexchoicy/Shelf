using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shelf.Core.Services;
using Shelf.Infrastructure.Authentication;
using Shelf.Infrastructure.Data;
using Shelf.Infrastructure.Entity;
using Shelf.Infrastructure.Services;
using Shelf.Infrastructure.StorageServices;
using Shelf.Core.StorageServices;

namespace Shelf.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContextPool<AppDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration["Database:ConnectionString"]);
        });

        services.AddIdentityCore<User>(opt =>
        {
            if (environment.IsDevelopment())
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 4;
            }
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IMeService, MeService>();

        services.AddSingleton<LocalStorageProvider>();
        services.AddSingleton<IStorageProvider>(sp => sp.GetRequiredService<LocalStorageProvider>());
        services.AddSingleton<IStorageProviderResolver, StorageProviderResolver>();

        services.AddScoped<ILocalStorageService, LocalStorageService>();

        return services;
    }
}
