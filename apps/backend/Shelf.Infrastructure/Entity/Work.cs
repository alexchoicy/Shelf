using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Enum;


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

    public string NovelContent { get; set; } = string.Empty;

    public WorkType Type { get; set; }
    public WorkVisibility Visibility { get; set; } = WorkVisibility.PUBLIC;
    public WorkRating Rating { get; set; } = WorkRating.GENERAL;

    public Guid? CoverFileId { get; set; }
    public File? CoverFile { get; set; }

    public DateTime? ReleasedAt { get; set; } = null;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}