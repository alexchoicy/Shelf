using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Entity;
using Shelf.Core.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shelf.Core.Utils;

namespace Shelf.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Core.Entity.File> Files { get; set; }
    public DbSet<MediaVariant> MediaVariants { get; set; }
    public DbSet<MediaAsset> MediaAssets { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<PartyAccount> PartyAccounts { get; set; }
    public DbSet<PartyAlias> PartyAliases { get; set; }
    public DbSet<PartyCover> PartyCovers { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<WorkCover> WorkCovers { get; set; }
    public DbSet<WorkCredit> WorkCredits { get; set; }
    public DbSet<WorkSource> WorkSources { get; set; }

    public DbSet<WorkCharacter> WorkCharacters { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterAlias> CharacterAliases { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Id = "39773055-af77-4687-afc5-ceb1d99b5a8e",
                Name = Core.Enum.Roles.Admin.ToString(),
                NormalizedName = Core.Enum.Roles.Admin.ToString().ToUpper(),
                ConcurrencyStamp = "508a0eaf-dbca-47d9-baeb-597b81a4957e"
            },
            new IdentityRole
            {
                Id = "e1368ff1-fb86-4763-8bd7-eb5a4269084e",
                Name = Core.Enum.Roles.User.ToString(),
                NormalizedName = Core.Enum.Roles.User.ToString().ToUpper(),
                ConcurrencyStamp = "70b645e2-64b9-4d69-8a37-46413af238b0"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }

    public override int SaveChanges()
    {
        NormalizeEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        NormalizeEntities();
        return base.SaveChangesAsync(cancellationToken);
    }



    private void NormalizeEntities()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
            .Where(entry => entry.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            switch (entry.Entity)
            {
                case Party party:
                    party.NormalizedName = StringUtils.NormalizeString(party.Name);
                    break;
                case PartyAlias partyAlias:
                    partyAlias.NormalizedName = StringUtils.NormalizeString(partyAlias.Name);
                    break;
                case Character character:
                    character.NormalizedName = StringUtils.NormalizeString(character.Name);
                    break;
                case CharacterAlias characterAlias:
                    characterAlias.NormalizedName = StringUtils.NormalizeString(characterAlias.Name);
                    break;
                case Series series:
                    series.NormalizedName = StringUtils.NormalizeString(series.Name);
                    break;
                case Source source:
                    source.NormalizedName = StringUtils.NormalizeString(source.Name);
                    break;
            }
        }
    }
}
