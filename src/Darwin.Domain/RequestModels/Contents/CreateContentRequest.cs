namespace Darwin.Domain.RequestModels.Contents;

public record CreateContentRequest(string Name, string Lyrics, string ImageUrl, bool IsUsable, IList<Guid> CategoryIds, IList<Guid> MoodIds);
