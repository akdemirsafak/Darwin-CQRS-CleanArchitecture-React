namespace Darwin.Model.Request.Musics;

public record CreateMusicRequest(string Name, string Lyrics, string ImageUrl, bool IsUsable, IList<Guid> CategoryIds, IList<Guid> MoodIds, Guid AgeRateId);
