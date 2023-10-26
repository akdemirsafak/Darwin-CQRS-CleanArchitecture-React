namespace Darwin.Model.Request.PlayLists;

public record AddContentToPlayListRequest(Guid playListId, Guid contentId);
