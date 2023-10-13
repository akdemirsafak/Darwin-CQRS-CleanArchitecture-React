namespace Darwin.Service.UserHelper;

public interface ICurrentUser
{
    public string GetUserId { get; }
    public int UserAge { get; }
}
