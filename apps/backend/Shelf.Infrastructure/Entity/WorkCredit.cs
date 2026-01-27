using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shelf.Infrastructure.Entity;

[Table("WorkCredits")]
[PrimaryKey(nameof(Id))]
public class WorkCredit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public required Guid PartyId { get; set; }
    public Party? Party { get; set; }
    public int CreditRoleId { get; set; }
    public CreditRole? CreditRole { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;
}