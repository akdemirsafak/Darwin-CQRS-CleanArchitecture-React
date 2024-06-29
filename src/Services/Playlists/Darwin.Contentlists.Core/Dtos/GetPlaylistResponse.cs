namespace Darwin.Contentlists.Core.Dtos;

public sealed class GetPlaylistResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public string CreatorName { get; set; }

    public Guid? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public Guid? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }

    public List<Guid>? ContentIds { get; set; }
}
