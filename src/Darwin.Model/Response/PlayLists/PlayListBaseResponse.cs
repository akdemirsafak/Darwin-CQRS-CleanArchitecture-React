namespace Darwin.Model.Response.PlayLists;

public class PlayListBaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public bool IsUsable { get; set; }

}
