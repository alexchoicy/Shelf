using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;


namespace Shelf.Infrastructure.Entity;

[Table("Work")]
[PrimaryKey(nameof(Id))]
public class Work
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string UploaderId { get; set; }
    public User Uploader { get; set; } = default!;

    public required string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsAI { get; set; } = false;

    public WorkMedium Medium { get; set; }
    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;
    public WorkRating Rating { get; set; } = WorkRating.GENERAL;

    public DateTimeOffset? ReleasedAt { get; set; } = null;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; } = null;


    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
    public ICollection<WorkCover> Covers { get; set; } = new List<WorkCover>();
    public ICollection<SeriesWorks> SeriesWorks { get; set; } = new List<SeriesWorks>();
    public ICollection<WorkTag> WorkTags { get; set; } = new List<WorkTag>();
    public ICollection<WorkCredit> WorkCredits { get; set; } = new List<WorkCredit>();
    public ICollection<WorkSource> WorkSources { get; set; } = new List<WorkSource>();
}
