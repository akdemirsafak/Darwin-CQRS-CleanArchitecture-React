using Darwin.Application.Services;
using Darwin.Domain.Dtos.Youtube;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Darwin.Infrastructure.Services;

public class YoutubeService : IYoutubeService
{
    public async Task<List<YoutubeApiResponse>> GetChannelContentsAsync(string pageToken, int size)
    {
        YouTubeService youtubeService = new(new BaseClientService.Initializer
        {
            ApiKey = "",
            ApplicationName = "YoutubeApi"
        });
        var searchRequest = youtubeService.Search.List("snippet");
        searchRequest.ChannelId = "UC0WP5P-ufpRfjbNrmOWwLBQ";
        searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

        searchRequest.MaxResults = size;
        searchRequest.PageToken = pageToken;

        var searchResponse = await searchRequest.ExecuteAsync();
        var contentList = searchResponse.Items.Select(content => new YoutubeApiResponse()
        {
            Title = content.Snippet.Title,
            Link = $"https://www.youtube.com/watch?v={content.Id.VideoId}",
            ThumbnailImage = content.Snippet.Thumbnails.Medium.Url,
            PublishedAt = content.Snippet.PublishedAtDateTimeOffset

        })
            .OrderByDescending(video => video.PublishedAt)
            .ToList();
        return contentList;
    }
}
