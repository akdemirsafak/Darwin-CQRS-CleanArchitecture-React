using Darwin.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Darwin.AuthServer.Entities;

public class AppRole : IdentityRole, IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public string? DeletedBy { get; set; }
}
