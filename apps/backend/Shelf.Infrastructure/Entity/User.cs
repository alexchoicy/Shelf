using Microsoft.AspNetCore.Identity;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Entity;

public class User : IdentityUser
{
    public ICollection<Work> UploadedWorks { get; set; } = new List<Work>();
}
