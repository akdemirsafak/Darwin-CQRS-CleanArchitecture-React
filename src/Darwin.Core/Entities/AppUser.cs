
using Microsoft.AspNetCore.Identity;

namespace Darwin.Core.Entities;

public class AppUser : IdentityUser, IAuditableEntity
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public virtual ICollection<PlayList> PlayLists { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
}
