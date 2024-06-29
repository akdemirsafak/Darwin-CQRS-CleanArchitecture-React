namespace Darwin.Contentlists.Core.Dtos;

public record UpdatePlaylistRequest(string name, bool isPublic, List<Guid>? contentIds, string? description);