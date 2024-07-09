namespace Darwin.Contentlists.Core.Dtos;

public sealed class GetPlaylistResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string CreatorName { get; set; }

    public string? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public Guid? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }

    public List<Guid>? ContentIds { get; set; }
}
