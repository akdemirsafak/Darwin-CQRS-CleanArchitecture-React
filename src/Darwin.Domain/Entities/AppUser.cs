using Microsoft.AspNetCore.Identity;

namespace Darwin.Domain.Entities;

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
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get ; set ; }
    public DateTime? DeletedOnUtc { get; set; }
    public string? DeletedBy { get ; set ; }
}
