using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Shelf.Infrastructure.Entity;

//TODO: This things have seed
[Table("PartyAccounts")]
[PrimaryKey(nameof(Id))]
public class PartyAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }

    public required int SourceId { get; set; }
    public Source? Source { get; set; }

    public string ExternalId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}