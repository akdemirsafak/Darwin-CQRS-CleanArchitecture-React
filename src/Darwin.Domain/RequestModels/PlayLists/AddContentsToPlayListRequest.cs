namespace Darwin.Domain.RequestModels.PlayLists;

public record AddContentsToPlayListRequest(Guid playListId, IList<Guid> contentIds);
