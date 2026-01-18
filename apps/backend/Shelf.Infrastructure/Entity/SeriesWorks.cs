using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("SeriesWorks")]
[PrimaryKey(nameof(SeriesId), nameof(WorkId))]
public class SeriesWorks
{

    public int SeriesId { get; set; }
    public Series? Series { get; set; }

    public Guid WorkId { get; set; }
    public Work? Work { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}