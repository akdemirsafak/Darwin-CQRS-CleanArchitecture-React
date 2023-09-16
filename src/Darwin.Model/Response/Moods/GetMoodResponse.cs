namespace Darwin.Model.Response.Moods;

public class GetMoodResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
