using Darwin.Model.Response.Categories;
using Darwin.Model.Response.ContentAgeRates;
using Darwin.Model.Response.Moods;

namespace Darwin.Model.Response.Musics;

public class GetMusicByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
    public virtual GetAgeRateResponse AgeRate { get; set; }
    public virtual IList<GetMoodResponse> Moods { get; set; }
    public virtual IList<GetCategoryResponse> Categories { get; set; }
}
