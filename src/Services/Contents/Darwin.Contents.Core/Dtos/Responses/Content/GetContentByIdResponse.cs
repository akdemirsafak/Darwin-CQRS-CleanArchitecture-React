using Darwin.Contents.Core.Dtos.Responses.Category;
using Darwin.Contents.Core.Dtos.Responses.Mood;

namespace Darwin.Contents.Core.Dtos.Responses.Content;


public class GetContentByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public string ImageUrl { get; set; }
    public virtual IList<GetMoodResponse> Moods { get; set; }
    public virtual IList<GetCategoryResponse> Categories { get; set; }
}
