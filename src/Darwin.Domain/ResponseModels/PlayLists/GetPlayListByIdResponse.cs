using Darwin.Domain.ResponseModels.Contents;

namespace Darwin.Domain.ResponseModels.PlayLists;

public class GetPlayListByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsUsable { get; set; }
    public bool IsPublic { get; set; }
    public virtual IList<GetContentResponse> Contents { get; set; }
}
