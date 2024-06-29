namespace Darwin.Contentlists.Core.Dtos;

public record CreatePlaylistRequest(string name, List<Guid>? contentIds, bool? isPublic,string? description);