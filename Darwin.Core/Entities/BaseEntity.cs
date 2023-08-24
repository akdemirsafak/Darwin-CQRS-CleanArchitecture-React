using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public long CreatedAt { get; set; }
    public long? UpdatedAt { get; set; }
    public long? DeletedAt { get; set; }
    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow.Ticks;
    }
}
