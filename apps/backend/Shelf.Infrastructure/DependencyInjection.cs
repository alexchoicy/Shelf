using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shelf.Core.Services;
using Shelf.Infrastructure.Authentication;
using Shelf.Infrastructure.Data;
using Shelf.Infrastructure.Entity;
using Shelf.Infrastructure.Services;
using Shelf.Core.Models;
using Shelf.Infrastructure.Services.Character;
using Shelf.Infrastructure.Services.Party;
using Shelf.Infrastructure.Services.Work;
using Shelf.Core.Enum;
using Amazon.S3;
using Shelf.Infrastructure.Services.Storage;

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

        services.AddScoped<IWorkService, WorkService>();
        services.AddScoped<IPartyService, PartyService>();
        services.AddScoped<ICharacterService, CharacterService>();

        services.Configure<StorageOptions>(configuration.GetSection("Storage"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<StorageOptions>>().Value.MediaFolders);

        StorageOptions storage = configuration
            .GetSection("Storage")
            .Get<StorageOptions>()
            ?? throw new InvalidOperationException("Storage config missing");


        if (storage.DefaultProvider == StorageProvider.S3)
        {
            services.AddSingleton(_ =>
            {
                var opt = storage.S3 ?? throw new InvalidOperationException("Assets S3 settings missing.");
                var cfg = new AmazonS3Config
                {
                    ServiceURL = opt.Endpoint,
                    ForcePathStyle = true,
                    AuthenticationRegion = opt.Region,
                };

                return new AmazonS3Client(opt.AccessKey, opt.SecretKey, cfg);
            });
        }

        services.AddScoped<S3StorageService>();

        services.AddScoped<IStorageService>(sp =>
            {
                var provider = storage.DefaultProvider;
                return provider switch
                {
                    StorageProvider.S3 => sp.GetRequiredService<S3StorageService>(),
                    _ => throw new NotSupportedException($"Storage provider {provider} is not supported.")
                };
            });


        return services;
    }
}
