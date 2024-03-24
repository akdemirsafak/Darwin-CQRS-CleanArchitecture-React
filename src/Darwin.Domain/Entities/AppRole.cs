using Microsoft.AspNetCore.Identity;

namespace Darwin.Domain.Entities;

public class AppRole : IdentityRole, IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public string? UpdatedBy { get; set; }
}
