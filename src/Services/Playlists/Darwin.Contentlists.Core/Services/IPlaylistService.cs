using Darwin.Contentlists.Core.Dtos;
using Darwin.Shared.Dtos;

namespace Darwin.Contentlists.Core.Services;

public interface IPlaylistService
{

    Task<DarwinResponse<NoContentDto>> CreateAsync(CreatePlaylistRequest request);
    Task<DarwinResponse<List<GetPlaylistResponse>>> GetAllAsync();
    Task<DarwinResponse<GetPlaylistResponse>> GetByIdAsync(Guid id);
    Task<DarwinResponse<NoContentDto>> UpdateAsync(Guid id, UpdatePlaylistRequest request);
    Task<DarwinResponse<NoContentDto>> DeleteAsync(Guid id);
}
