using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shelf.Api.ExceptionHandlers;
using Shelf.Core.Enum;
using Shelf.Infrastructure;
using Shelf.Infrastructure.Data;
using Shelf.Infrastructure.Data.Seed;
using Shelf.Infrastructure.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = options =>
    {
        options.ProblemDetails.Extensions.TryAdd("traceId",
            options.HttpContext.TraceIdentifier);
        options.ProblemDetails.Extensions.TryAdd("timestamp",
            DateTime.UtcNow);
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p => p
        .WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)
        )
    };

    // Bro will solve Bearer JWT in [Authorization] by default so cool.
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("AlexCoolShelfAppToken", out string? authToken))
            {
                context.Token = authToken;
            }
            else if (context.Request.Query.TryGetValue("access_token", out var accessTokenValues))
            {
                context.Token = accessTokenValues;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserAllowed",
        policy => policy.RequireClaim("access_type", TokenUseType.USERACCESS.ToString()));
    options.DefaultPolicy = options.GetPolicy("UserAllowed")!;

    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(Roles.Admin.ToString()));

    options.AddPolicy("ShareAllowed", policy =>
        policy.RequireClaim("access_type",
            TokenUseType.USERACCESS.ToString(),
            TokenUseType.CONTENTACCESS.ToString()));
});

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await UserSeed.SeedAsync(userManager, builder.Configuration);

        //optional later
        //Create a "Unknown" party for works without a known primary party
        await PartySeed.SeedAsync(dbContext);
    }
}

app.UseExceptionHandler();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
