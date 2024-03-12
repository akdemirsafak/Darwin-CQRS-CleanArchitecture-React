using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.Contents;
using Darwin.Domain.ResponseModels.Contents;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class ContentMapper
{
    public partial UpdatedContentResponse ContentToUpdatedContentResponse(Content content);
    public partial CreatedContentResponse ContentToCreatedContentResponse(Content content);
    public partial Content CreateContentRequestToContent(CreateContentRequest content);
    public partial List<GetContentResponse> GetContentResponseListToContentList(List<Content> contentList);
}
