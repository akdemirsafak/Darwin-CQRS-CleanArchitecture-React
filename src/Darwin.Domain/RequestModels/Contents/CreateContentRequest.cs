namespace Darwin.Domain.RequestModels.Contents;

public record CreateContentRequest(string Name, string Lyrics, string ImageUrl, bool? IsUsable=true, IList<Guid> CategoryIds, IList<Guid> MoodIds);
