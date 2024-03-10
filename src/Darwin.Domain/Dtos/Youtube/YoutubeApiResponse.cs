namespace Darwin.Domain.Dtos.Youtube;

public class YoutubeApiResponse
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string ThumbnailImage { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
}
