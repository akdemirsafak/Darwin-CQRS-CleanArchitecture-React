namespace Darwin.Service.Helper;

public interface ILinkCreator
{
    Task<string> CreateTokenMailUrl(string action, string controller, string userId, string token);
}
