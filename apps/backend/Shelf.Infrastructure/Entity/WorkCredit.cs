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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}