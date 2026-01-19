
using Microsoft.AspNetCore.Identity;

namespace Shelf.Infrastructure.Entity;

public class User : IdentityUser
{

    public ICollection<Work> UploadedWorks { get; set; } = new List<Work>();
    public ICollection<WorkTag> AssertedWorkTags { get; set; } = new List<WorkTag>();
}