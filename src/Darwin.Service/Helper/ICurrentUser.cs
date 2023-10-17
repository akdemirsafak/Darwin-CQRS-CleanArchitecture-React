namespace Darwin.Service.Helper;

public interface ICurrentUser
{
    public string GetUserId { get; }
    public int UserAge { get; }
}
