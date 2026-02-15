using Shelf.Core.Enum;


namespace Shelf.Core.Entity;

//TODO: This things have seed
public class Source
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Url { get; set; }

    public string NormalizedName { get; set; } = string.Empty;

    public SourceIconKey IconKey { get; set; } = SourceIconKey.GENERIC;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<WorkSource> WorkSources { get; set; } = new List<WorkSource>();
    public ICollection<PartyAccount> PartyAccounts { get; set; } = new List<PartyAccount>();
}
