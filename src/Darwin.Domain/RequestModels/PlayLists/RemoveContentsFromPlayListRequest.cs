
namespace Darwin.Domain.RequestModels.PlayLists;

public record RemoveContentsFromPlayListRequest(Guid playListId, IList<Guid> contentIds);