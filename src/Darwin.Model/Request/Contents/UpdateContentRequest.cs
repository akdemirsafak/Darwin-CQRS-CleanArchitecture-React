namespace Darwin.Model.Request.Contents;

public record UpdateContentRequest(string Name, string Lyrics, string ImageUrl, bool IsUsable, IList<Guid> CategoryIds, IList<Guid> MoodIds);