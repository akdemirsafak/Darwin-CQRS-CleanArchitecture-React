namespace Darwin.Model.Response.Musics;

public class GetMusicResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Publishers { get; set; }
    public bool IsUsable { get; set; }
}
