using Darwin.Domain.Entities;
using Darwin.Domain.RequestModels.PlayLists;
using Darwin.Domain.ResponseModels.PlayLists;
using Riok.Mapperly.Abstractions;

namespace Darwin.Persistance.Mapping;

[Mapper]
public partial class PlayListMapper
{
    public partial CreatedPlayListResponse PlayListToCreatedPlayListResponse(PlayList playList);
    public partial PlayList CreatePlayListRequestToPlayList(CreatePlayListRequest createPlayListRequest);
    public partial UpdatedPlayListResponse PlayListToUpdatedPlayListResponse(PlayList playList);
    public partial GetPlayListByIdResponse PlayListToGetPlayListByIdResponse(PlayList playList);

}
