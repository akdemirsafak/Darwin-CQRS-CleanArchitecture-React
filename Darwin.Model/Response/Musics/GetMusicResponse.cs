namespace Darwin.Model.Response.Musics;

public class GetMusicResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
