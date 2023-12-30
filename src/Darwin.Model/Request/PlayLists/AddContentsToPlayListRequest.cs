namespace Darwin.Model.Request.PlayLists;

public record AddContentsToPlayListRequest(Guid playListId, IList<Guid> contentIds);
