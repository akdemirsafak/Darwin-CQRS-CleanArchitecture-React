namespace Darwin.Web.ViewModels;

public class MoodViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    //public Guid CreatedBy { get; set; }
    //public Guid UpdatedBy { get; set; }
}
