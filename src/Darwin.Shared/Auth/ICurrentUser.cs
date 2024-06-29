namespace Darwin.Shared.Auth;

public interface ICurrentUser
{
    public string GetUserId { get; }
    public string GetUserName { get; }
}
