namespace Darwin.Contentlists.Core.Entities;

public sealed class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public List<Guid>? ContentIds { get; set; }
    public string CreatorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set ; }
    public string? DeletedBy { get; set ; }
    public bool IsDeleted { get ; set ; }
}
