using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;


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
        set => _normalizedName = value.Normalize();
    }

    public required PartyType Type { get; set; } = PartyType.INDIVIDUAL;

    public Guid? CoverFileId { get; set; }
    public File? CoverFile { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<PartyAlias> Aliases { get; set; } = new List<PartyAlias>();
    public ICollection<PartyAccount> Accounts { get; set; } = new List<PartyAccount>();
    public ICollection<WorkCredit> WorkCredits { get; set; } = new List<WorkCredit>();
    public ICollection<Work> PrimaryWorks { get; set; } = new List<Work>();

}