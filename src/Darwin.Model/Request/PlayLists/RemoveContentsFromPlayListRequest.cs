
namespace Darwin.Model.Request.PlayLists;

public record RemoveContentsFromPlayListRequest(Guid playListId, IList<Guid> contentIds);