namespace Darwin.Model.Request.PlayLists;

public record AddMusicToPlayListRequest(Guid playListId, Guid musicId);
