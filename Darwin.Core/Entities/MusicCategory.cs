namespace Darwin.Core.Entities;

public class MusicCategory:BaseEntity
{
    public Guid MusicId { get; set; }
    public Music Music { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
