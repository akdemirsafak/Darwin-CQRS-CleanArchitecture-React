namespace Darwin.Model.Response.Moods;

public class UpdatedMoodResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
