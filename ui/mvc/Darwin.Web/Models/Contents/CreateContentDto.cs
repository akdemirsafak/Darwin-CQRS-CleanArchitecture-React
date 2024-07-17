namespace Darwin.Web.Models.Contents;

public class CreateContentDto
{
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public IFormFile ImageFile { get; set; }
    public IList<Guid> SelectedCategories { get; set; }
    public IList<Guid> SelectedMoods { get; set; }
}