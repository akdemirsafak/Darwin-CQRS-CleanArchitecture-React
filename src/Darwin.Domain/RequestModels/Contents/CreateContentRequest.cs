namespace Darwin.Domain.RequestModels.Contents;

public record CreateContentRequest(string Name, string Lyrics, string ImageUrl, IList<Guid> CategoryIds, IList<Guid> MoodIds, bool IsUsable=true);
