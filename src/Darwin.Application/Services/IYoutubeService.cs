using Darwin.Domain.Dtos.Youtube;

namespace Darwin.Application.Services;

public interface IYoutubeService
{
    Task<List<YoutubeApiResponse>> GetChannelContentsAsync(string pageToken, int size);
}
