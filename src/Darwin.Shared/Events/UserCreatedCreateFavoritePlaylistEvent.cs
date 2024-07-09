namespace Darwin.Shared.Events;

public class UserCreatedCreateFavoritePlaylistEvent
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedDate { get; set; }

}
