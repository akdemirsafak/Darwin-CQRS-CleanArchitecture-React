
using Microsoft.AspNetCore.Identity;

namespace Darwin.Core.Entities;

public class AppUser : IdentityUser
{
    public long? LastLogin { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
