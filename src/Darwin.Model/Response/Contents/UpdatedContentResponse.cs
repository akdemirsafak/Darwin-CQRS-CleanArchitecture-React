namespace Darwin.Model.Response.Contents;

public class UpdatedContentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
