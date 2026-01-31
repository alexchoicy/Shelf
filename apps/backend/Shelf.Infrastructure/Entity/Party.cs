using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;
using Shelf.Infrastructure.Extensions;


namespace Shelf.Infrastructure.Entity;

//TODO: This things have seed
[Table("Parties")]
[PrimaryKey(nameof(Id))]
public class Party
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string Name { get; set; }
    private string _normalizedName = string.Empty;
    public string NormalizedName
    {
        get => _normalizedName;
        set => _normalizedName = StringExtensions.Normalize(value);
    }

    public required PartyType Type { get; set; } = PartyType.INDIVIDUAL;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<PartyAlias> Aliases { get; set; } = new List<PartyAlias>();
    public ICollection<PartyAccount> Accounts { get; set; } = new List<PartyAccount>();
    public ICollection<PartyCover> Covers { get; set; } = new List<PartyCover>();
    public ICollection<WorkCredit> WorkCredits { get; set; } = new List<WorkCredit>();
    public ICollection<Work> PrimaryWorks { get; set; } = new List<Work>();

}
