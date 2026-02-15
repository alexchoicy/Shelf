using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class Party
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Name { get; set; }

    public string NormalizedName { get; set; } = string.Empty;

    public required PartyType Type { get; set; } = PartyType.Individual;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<PartyAlias> Aliases { get; set; } = new List<PartyAlias>();
    public ICollection<PartyAccount> Accounts { get; set; } = new List<PartyAccount>();
    public ICollection<PartyCover> Covers { get; set; } = new List<PartyCover>();
    public ICollection<WorkCredit> WorkCredits { get; set; } = new List<WorkCredit>();
    public ICollection<Character> CreatedCharacters { get; set; } = new List<Character>();
}
