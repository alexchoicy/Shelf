using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;


namespace Shelf.Infrastructure.Entity;

//TODO: This things have seed
[Table("Sources")]
[PrimaryKey(nameof(Id))]
public class Source
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Url { get; set; }
    private string _normalizedName = string.Empty;
    public string NormalizedName
    {
        get => _normalizedName;
        set => _normalizedName = value.Normalize();
    }

    public SourceIconKey IconKey { get; set; } = SourceIconKey.GENERIC;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public ICollection<PartyAccount> PartyAccounts { get; set; } = new List<PartyAccount>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<WorkSource> WorkSources { get; set; } = new List<WorkSource>();
}