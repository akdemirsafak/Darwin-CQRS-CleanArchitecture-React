namespace Darwin.Shared.Auth;

public interface ICurrentUser
{
    public string GetUserId { get; }
}
