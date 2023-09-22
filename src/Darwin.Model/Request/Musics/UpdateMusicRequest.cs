namespace Darwin.Model.Request.Musics;

public record UpdateMusicRequest(string Name, string ImageUrl, bool IsUsable, IList<Guid> CategoryIds, IList<Guid> MoodIds);