
using Microsoft.AspNetCore.Identity;

namespace Darwin.Core.Entities;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public long? LastLogin { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<PlayList> PlayLists { get; set; }
}
