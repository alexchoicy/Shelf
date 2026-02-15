using Shelf.Core.Enum;

namespace Shelf.Core.Entity;

public class Work
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string UploaderId { get; set; }

    public required string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsAI { get; set; } = false;

    public WorkMedium Medium { get; set; }
    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.Public;

    public WorkRating Rating { get; set; } = WorkRating.General;

    public DateTimeOffset? ReleasedAt { get; set; } = null;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
    public ICollection<WorkCover> Covers { get; set; } = new List<WorkCover>();
    public ICollection<WorkCredit> WorkCredits { get; set; } = new List<WorkCredit>();
    public ICollection<WorkSource> WorkSources { get; set; } = new List<WorkSource>();
    public ICollection<WorkCharacter> WorkCharacters { get; set; } = new List<WorkCharacter>();
}
