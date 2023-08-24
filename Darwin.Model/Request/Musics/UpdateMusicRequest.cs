namespace Darwin.Model.Request.Musics;

public record UpdateMusicRequest(string Name, string Url, string Publishers,bool IsUsable);