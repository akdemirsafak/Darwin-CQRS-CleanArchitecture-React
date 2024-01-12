using Darwin.Core.BaseDto;
using Darwin.Service.Common;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Darwin.Service.ExternalService.YoutubeApi.GetTheWeekend;

public static class GetTheWeekendChannel
{
    public record Command(string PageToken, int Size) : ICommand<DarwinResponse<List<YoutubeApiResponse>>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<List<YoutubeApiResponse>>>
    {
        public async Task<DarwinResponse<List<YoutubeApiResponse>>> Handle(Command request, CancellationToken cancellationToken)
        {
            YouTubeService youtubeService = new(new BaseClientService.Initializer
            {
                ApiKey = "HereApiKey",
                ApplicationName = "YoutubeApi"
            });
            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.ChannelId = "UC0WP5P-ufpRfjbNrmOWwLBQ";
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            searchRequest.MaxResults = request.Size;
            searchRequest.PageToken = request.PageToken;

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

            return DarwinResponse<List<YoutubeApiResponse>>.Success(contentList, 200);

        }
    }
}