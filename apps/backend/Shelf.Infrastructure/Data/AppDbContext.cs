using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CreditRole> CreditRoles { get; set; }
    public DbSet<Entity.File> Files { get; set; }
    public DbSet<MediaItem> MediaItems { get; set; }
    public DbSet<AIModel> AIModels { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<PartyAccount> PartyAccounts { get; set; }
    public DbSet<PartyAlias> PartyAliases { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<SeriesWorks> SeriesWorks { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagAlias> TagAliases { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<WorkCredit> WorkCredits { get; set; }
    public DbSet<WorkSource> WorkSources { get; set; }
    public DbSet<WorkTag> WorkTags { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}