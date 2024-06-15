using Darwin.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Darwin.AuthServer.Entities;

public class AppUser : IdentityUser, IAuditableEntity
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
